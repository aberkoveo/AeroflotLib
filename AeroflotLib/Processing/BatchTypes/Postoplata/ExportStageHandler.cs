using ABBYY.FlexiCapture;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FC12;
using AeroflotLib.Processing.ExportSap;

namespace AeroflotLib.Processing.BatchTypes.Postoplata
{
    public class ExportStageHandler
    {
        private readonly ILogger _logger;
        private IDocument Document { get; set; }
        private readonly SapExportModule SapExportModule;
        private readonly IProcessingCallback _processing;
        public ExportStageHandler(IDocument document, IProcessingCallback processing)
        {
            Document = document;
            _processing = processing;
            _logger = new FC12Logger(document, processing).logger;
            SapExportModule = new SapExportModule(document, processing);
        }

        public void Handle()
        {

            

            //string obj_key = "";     // номер ДКЗ + год (регистрационные параметры комплекта)
            string batchnum = "";   // номер пакетаы

            //string doctype = "";    // тип документа
            //string objtype = "";    // BUS2081 или SCASE 
            //string filename = "";   // имя файла

            string docnum = "";    // Номер документа (акта, СФ, счета и тд)

            int err = 0;    // переменная для проверки успешного экспорта документов   
            if (Document.Properties.Get("Отправлено") != "X" && (Document.DefinitionName == "Постоплата РФ" || Document.DefinitionName == "Постоплата Зарубеж"))
            {

                _processing.ReportMessage($"Комплект {Document.Id} будет экспортирован.");
                // Определение номера пакета
                batchnum = Document.Batch.Id.ToString();
                while (batchnum.Length < 4)
                {
                    batchnum = "0" + batchnum;
                }
                // Определение номера комплекта
                string setnum = Document.Id.ToString();
                while (setnum.Length < 8)
                {
                    setnum = "0" + setnum;
                }

                // Определение ключа - номер ДКЗ + год ДКЗ, включая корректировочные СФ

                // Создаем список ДКЗ (чтобы исключить дубли)
                List<string> DKZ_list_general = new List<string> { };
                DKZ_list_general.Clear();

                // Создаем список СФ (чтобы исключить дубли)
                List<string> SF_list = new List<string> { };
                SF_list.Clear();

                for (int i = 0; i < Document.Field("Сводный раздел\\ResultTable").Rows.Count; i++)
                {
                    string dkz_number = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Belnr").Text;
                    string dkz_year = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Gjahr").Text;
                    string doc_num = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\DocNum").Text;
                    string ext_key = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Ext_key").Text;
                    string docnum_sf = "";
                    if (Document.DefinitionName == "Постоплата РФ")
                    {
                        docnum_sf = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\DocNum_SF").Text;
                        // Проверяем, не было ли еще такой СФ
                        if (docnum_sf != "" && !SF_list.Contains(docnum_sf) && !doc_num.Contains("F"))
                            SF_list.Add(docnum_sf);
                        else
                            docnum_sf = "";

                        // Проверяем случай с пени (СФ прикреплять не нужно)
                        if (doc_num.Contains("F"))
                            docnum_sf = "";
                    }



                    string[] req = new string[4];
                    string fullpath = Document.Batch.Project.EnvironmentVariables.Get("Export_path") + batchnum + "_" + setnum;
                    string[] filesname = Directory.GetFiles(fullpath);

                    //=====================    
                    string str = "";
                    foreach (string file in filesname)
                    {
                        str += file;
                    }
                    //Document.Properties.Set("Test2", str);
                    //=====================  

                    //Send_File obj = new Send_File();
                    

                    Dictionary<string, string> scans = new Dictionary<string, string>();

                    scans.Add("TORG-12" + doc_num + ".pdf", "2");
                    scans.Add("VATInvoice" + docnum_sf + ".pdf", "1");
                    scans.Add("VATInvoiceCorrect" + docnum_sf + ".pdf", "9");
                    scans.Add("VATInvoiceChange" + docnum_sf + ".pdf", "13");
                    scans.Add("Account" + doc_num + ".pdf", "7");
                    scans.Add("Act" + doc_num + ".pdf", "3");
                    scans.Add("UPD1" + doc_num + ".pdf", "UPD1");
                    scans.Add("UPD2" + doc_num + ".pdf", "UPD2");
                    scans.Add("UKD1" + doc_num + ".pdf", "UKD1");
                    scans.Add("UKD2" + doc_num + ".pdf", "UKD2");
                    scans.Add("Attachment.pdf", "OTH");
                    scans.Add("Foreign_invoice" + doc_num + ".pdf", "6");
                    scans.Add("Reestr.pdf", "OTH");
                    scans.Add("FormC.pdf", "OTH");
                    scans.Add("ReestrLU.pdf", "OTH");

                    string err_mes = "";    // переменная для вывода ошибок

                    bool AA_UPD = false;    // признак наличия акта или УПД

                    foreach (string filename in scans.Keys)
                    {
                        if (Array.IndexOf(filesname, fullpath + "\\" + filename) >= 0)
                        {
                            // ==============================================
                            // =================== ДКЗ ======================
                            // ==============================================
                            _logger.Debug($"Обработка файла {filename}");

                            if (dkz_number != "")
                            {
                                string objkey = dkz_number + dkz_year;

                                if (filename.Contains("Act") || filename.Contains("UPD") || filename.Contains("UKD") || filename.Contains("Foreign_invoice")) // проверяем, есть ли в ДКЗ Акт/УПД
                                {
                                    AA_UPD = true;
                                }

                                // прикладываем скан, если это не реестр, не акт по форме С или если в ДКЗ есть Акт/УПД   
                                if (
                                // если это приложение и ДКЗ не повторяется
                                (filename.Contains("Attachment") && DKZ_list_general.Contains(dkz_number) == false) ||
                                // если это не приложение, не реестр, не акт формы С - то есть обычный документ
                                (filename.Contains("Reestr") == false && filename.Contains("FormC") == false && filename.Contains("Attachment") == false) ||
                                // если это реестр или акт формы С, у нас есть акт/УПД, ДКЗ не повторяется
                                ((filename.Contains("Reestr") || filename.Contains("FormC")) && AA_UPD && DKZ_list_general.Contains(dkz_number) == false))
                                {
                                    Byte[] bytes = File.ReadAllBytes(fullpath + "\\" + filename);
                                    req[0] = Convert.ToBase64String(bytes);
                                    req[1] = scans[filename];
                                    req[2] = objkey;
                                    req[3] = "BUS2081";

                                    
                                    SapExportModule.ExportDocument(req);
                                    /*
                                    err_mes = obj.InvokeService(req);
                                    if (err_mes != "Save OK")
                                    {
                                        //Processing.ReportWarning("Ошибка экспорта C - " + err_mes);
                                        //IBS-logging
                                        logger.Error(err_mes);
                                        err++;
                                    }
                                    else
                                    {
                                        //IBS-logging
                                        logger.Info(err_mes);
                                    }*/
                                }
                            }

                            // ==============================================
                            // =================== КСч ======================
                            // ==============================================

                            if (ext_key != "" && filename.Contains("Attachment") == false && filename.Contains("Reestr") == false && filename.Contains("FormC") == false)
                            {
                                while (ext_key.Length < 12)
                                {
                                    ext_key = "0" + ext_key;
                                }
                                Byte[] bytes = File.ReadAllBytes(fullpath + "\\" + filename);
                                req[0] = Convert.ToBase64String(bytes);
                                req[1] = scans[filename];
                                req[2] = ext_key;
                                req[3] = "SCASE";

                                
                                SapExportModule.ExportDocument(req);
                                /*
                                err_mes = obj.InvokeService(req);
                                if (err_mes != "Save OK")
                                {
                                    //Processing.ReportWarning("Ошибка экспорта - " + err_mes);
                                    //IBS-logging
                                    logger.Error(err_mes);
                                    err++;
                                }
                                else
                                {
                                    //IBS-logging
                                    logger.Info(err_mes);
                                }*/

                                // Прикладываем счета ко всем ДКЗ
                                if (filename.Contains("Foreign_invoice") == false)    // не для инвойсов
                                {
                                    // Создаем список ДКЗ (чтобы исключить дубли)
                                    List<string> DKZ_list = new List<string> { };
                                    DKZ_list.Clear();

                                    for (int j = 0; j < Document.Field("Сводный раздел\\ResultTable").Rows.Count; j++)
                                    {
                                        string dkz_number_acc = Document.Field("Сводный раздел\\ResultTable[" + j.ToString() + "]\\Belnr").Text;
                                        string dkz_year_acc = Document.Field("Сводный раздел\\ResultTable[" + j.ToString() + "]\\Gjahr").Text;

                                        if (dkz_number_acc != "" && DKZ_list.Contains(dkz_number_acc) == false)
                                        {
                                            string objkey_acc = dkz_number_acc + dkz_year_acc;
                                            req[2] = objkey_acc;
                                            req[3] = "BUS2081";


                                            SapExportModule.ExportDocument(req);
                                            /*
                                            err_mes = obj.InvokeService(req);
                                            if (err_mes != "Save OK")
                                            {
                                                //Processing.ReportWarning("Ошибка экспорта - " + err_mes);
                                                //IBS-logging
                                                logger.Error(err_mes);
                                                err++;
                                            }
                                            else
                                            {
                                                //IBS-logging
                                                logger.Info(err_mes);
                                            }*/
                                        }

                                        DKZ_list.Add(dkz_number_acc);
                                    }
                                }
                            }
                        }
                    }

                    DKZ_list_general.Add(dkz_number);
                    Document.Properties.Set("Отправлено", "X");
                    /*
                    if (err == 0)
                        Document.Properties.Set("Отправлено", "X");
                    else
                    {
                        //Processing.ReportError("Не все документы были прикреплены к документу в SAP ERP");
                        logger.Error("Не все документы были прикреплены к документу в SAP ERP");
                    }*/
                }
            }
        }
    }
}
