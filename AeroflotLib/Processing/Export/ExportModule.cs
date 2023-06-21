using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using ABBYY.FlexiCapture;
using Microsoft.SqlServer.Server;
using FC12;

namespace AeroflotLib
{
    public class ExportModule
    {
        private readonly string ExportPathVar = "Export_pdf_path";
        private IExportImageSavingOptions ExportOptions { get; set; }
        private IDocument _Document { get; set; }
        private IProcessingCallback Processing { get; set; }
        private string DefinitionName { get; set; }
        private string BatchNumber { get; set; }
        private string DocumentNumber { get; set; }
        private string ExportRootPath { get; set; }
        private DocumentSections DocumentSections { get; set; } = new DocumentSections();
        private FC12Logger Logger { get; set; }

        public ExportModule(IDocument document, IProcessingCallback processing, IExportImageSavingOptions exportOptions)
        {
            this.ExportOptions = exportOptions;
            this._Document = document;
            this.Processing = processing;
            this.DefinitionName = this._Document.DefinitionName;
            this.BatchNumber = this._Document.Batch.Id.ToString("0000");
            this.DocumentNumber = this._Document.Id.PadLeft(8, '0');
            this.ExportRootPath = this._Document.Batch.Project.EnvironmentVariables.Get(this.ExportPathVar);
            this.Logger = new FC12Logger(document, processing);
        }
        
        public void ExportDocument() 
        {
            string filePath;

            //Экспорт документов комплекта
            if (DocumentSections.SetDocumentsTypes.Contains(DefinitionName) || 
                DefinitionName.Contains(DocumentSections.ForeignInvoice) ||
                DefinitionName.Contains(DocumentSections.PrepaymentInvoice))
            {
                string folderPath = CreateSetFolder(ExportRootPath, BatchNumber + "_" + GetSetNumber());

                switch (DefinitionName)
                {
                    case "ТОРГ-12":
                        filePath = folderPath + @"\TORG-12" + DocumentNumber + ".pdf";
                        break;

                    case "АктВыполненныхРабот":
                        // Добавляем единичку к номеру второго корректировочного документа
                        // (когда есть и уменьшение, и увеличение)
                        if (_Document.Field("АктВыполненныхРабот\\DocType").Text == "05")
                        {
                            Add1ToDocumentNumber();
                        }
                        filePath = folderPath + @"\Act" + DocumentNumber + ".pdf";
                        break;

                    case "Счет-фактура":
                        // не корректировочная и не исправительная
                        filePath = folderPath + @"\VATInvoice" + DocumentNumber + ".pdf";

                        // корректировочная
                        if (_Document.Field("Счет-фактура\\OriginalInvoiceNumber").IsVisible == true)
                        {
                            // Добавляем единичку к номеру второго корректировочного документа
                            // (когда есть и уменьшение, и увеличение)
                            if (_Document.Field("Счет-фактура\\DocType").Text == "05")
                            {
                                Add1ToDocumentNumber();
                            }
                            filePath = folderPath + @"\VATInvoiceCorrect" + DocumentNumber + ".pdf";
                        }

                        // исправительная
                        if (_Document.Field("Счет-фактура\\CorrectionNumber").IsVisible == true)
                        {
                            filePath = folderPath + @"\VATInvoiceChange" + DocumentNumber + ".pdf";
                        }

                        break;

                    case "Счет на оплату":
                        filePath = folderPath + @"\Account" + DocumentNumber + ".pdf";
                        //export
                        break;

                    case "УПД":
                        string statusUPD = _Document.Field("УПД\\Status").Text;
                        if (_Document.Field("УПД\\OriginalInvoiceNumber").IsVisible == false)
                        {
                            // УПД статус 1/2
                            filePath = folderPath + $@"\UPD{statusUPD}" + DocumentNumber + ".pdf";
                        }
                        else
                        {
                            // УКД статус 1/2
                            filePath = folderPath + $@"\UKD{statusUPD}" + DocumentNumber + ".pdf";
                        }

                        // Добавляем единичку к номеру второго корректировочного документа (когда есть и уменьшение, и увеличение)
                        if (_Document.Field("УПД\\DocType").Text == "05")
                        {
                            Add1ToDocumentNumber();
                            // УКД статус 1 или 2
                            filePath = folderPath + $@"\UKD{statusUPD}" + DocumentNumber + ".pdf";

                        }
                        break;

                    //Экспорт иностранные инвойсы
                    case string definition when definition.Contains("Инвойсы иностранные"):
                        filePath = folderPath + @"\Foreign_invoice" + DocumentNumber + ".pdf";
                        break;

                    case string definition when definition.Contains("Авансовый инвойс"):
                        filePath = folderPath + @"\Prepayment_invoice" + DocumentNumber + ".pdf";
                        break;

                    default:
                        this.Processing.ReportError("Тип определения документа не опознан");
                        Logger.logger.Error("Тип определения документа не опознан");
                        throw new Exception("Тип определения документа не опознан");
                }
                ExportPDF(filePath);
            }

            //Экспорт актов по форме С  ===================================================================
            if (_Document.Properties.Get("Тип комплекта") == "КЗУ Летные (Форма С)")
            {
                string folderPath = CreateSetFolder(ExportRootPath, BatchNumber + "_" + DocumentNumber);

                foreach (IPage page in _Document.Pages)
                {
                    if (page.SectionName != DocumentSections.ActFormC) page.ExcludedFromDocumentImage = true;
                }
                filePath = folderPath + @"\FormC" + ".pdf";

                ExportPDF(filePath);

                foreach (IPage page in _Document.Pages)
                {
                    page.ExcludedFromDocumentImage = false;
                }
            }

            //Экспорт реестров  ===========================================================================
            //Реестр ЛУ
            if (_Document.Properties.Get("Тип комплекта") == "КЗУ Летные (Реестр ЛУ Пасс)")
            {
                string folderPath = CreateSetFolder(ExportRootPath, BatchNumber + "_" + DocumentNumber);

                foreach (IPage page in _Document.Pages)
                {
                    if (page.SectionName != DocumentSections.ReestrLU) page.ExcludedFromDocumentImage = true;
                }
                filePath = folderPath + @"\ReestrLU" + ".pdf";

                ExportPDF(filePath);

                foreach (IPage page in _Document.Pages)
                {
                    page.ExcludedFromDocumentImage = false;
                }
            }

            //Экспорт Реестр иностранный 
            //Типы комплектов с реестрами
            string[] setTypes = { "КЗУ Летные (Menzies Aviation)", "КЗУ Летные (Swedavia ARN наземные)",
                                  "КЗУ Летные (Transportstyrelsen)", "КЗУ Летные (ICTS)",
                                  "КЗУ Летные (LMC SERVICES B.V.)", "КЗУ Летные (Amsterdam Airport Schiphol)" };

            if (setTypes.Contains(_Document.Properties.Get("Тип комплекта")))
            {
                string folderPath = CreateSetFolder(ExportRootPath, BatchNumber + "_" + DocumentNumber);

                foreach (IPage page in _Document.Pages)
                {
                    if (!DocumentSections.ReestrTypes.Contains(page.SectionName)) page.ExcludedFromDocumentImage = true;
                }

                filePath = folderPath + @"\Reestr" + ".pdf";

                ExportPDF(filePath);

                foreach (IPage page in _Document.Pages)
                {
                    page.ExcludedFromDocumentImage = false;
                }
            }

            //Экспорт приложений  ============================================================================
            if (_Document.DefinitionName == "Постоплата РФ" || _Document.DefinitionName == "Постоплата Зарубеж" ||
                _Document.DefinitionName == "Аванс РФ" || _Document.DefinitionName == "Аванс Зарубежный")
            {
                string folderPath = CreateSetFolder(ExportRootPath, BatchNumber + "_" + DocumentNumber);

                //Проверка на наличие приложений
                if (!(_Document.Pages is null))
                {
                    foreach (IPage page in _Document.Pages)
                    {
                        if (DocumentSections.GetAllSections().Contains(page.SectionName))
                        {
                            page.ExcludedFromDocumentImage = true;
                        }
                    }

                    filePath = folderPath + @"\Attachment" + ".pdf";
                    ExportPDF(filePath);

                    foreach (IPage page in _Document.Pages)
                    {
                        page.ExcludedFromDocumentImage = false;
                    }
                }
                else { Processing.ReportWarning($"{DefinitionName} не содержит страниц приложений"); }
            }
        }
         
        private void ExportPDF(string pdfFilePath)
        {
            try
            {
                _Document.SaveAs(pdfFilePath, ExportOptions);
                Logger.logger.Info( $"{DefinitionName} экспортирован успешно в {pdfFilePath}");
            }
            catch (Exception e)
            {
                this.Processing.ReportError(e.ToString());
                Logger.logger.Error($" Ошибка экспорта {DefinitionName} в {pdfFilePath} : {e.Message} : {e.StackTrace}");
            }
        }

        private string CreateSetFolder(string exportRootPath, string exportFolder)
        {
            string[] folderPathParts = { exportRootPath, exportFolder };
            string folderPath = Path.Combine(folderPathParts);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        //номер комплекта
        private string GetSetNumber()
        {
            return this._Document.AsBatchItem.Parent.AsDocument.Id.PadLeft(8, '0');
        }

        // Добавляем единичку к номеру второго корректировочного документа (когда есть и уменьшение, и увеличение)
        private void Add1ToDocumentNumber()
        {
            this.DocumentNumber = ("1" + _Document.Id).PadLeft(8, '0');
        }
    }
}
