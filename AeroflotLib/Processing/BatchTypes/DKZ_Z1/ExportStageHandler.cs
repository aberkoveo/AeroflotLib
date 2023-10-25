using ABBYY.FlexiCapture;
using AeroflotLib.Processing.ExportSap;
using FC12;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroflotLib.Processing.BatchTypes.DKZ_Z1
{
    public class ExportStageHandler
    {
        private readonly ILogger logger;
        private IDocument Document { get; set; }
        private readonly SapExportModule SapExportModule;
        public ExportStageHandler(IDocument document, IProcessingCallback processing)
        {
            Document = document;
            logger = new FC12Logger(document, processing).logger;
            SapExportModule = new SapExportModule(document, processing);
        }

        public void Handle()
        {

            //string obj_key = "";     // номер ДКЗ + год (регистрационные параметры комплекта)
            string batchnum = "";   // номер пакета

            //string doctype = "";    // тип документа
            //string objtype = "";    // BUS2081 или SCASE 
            //string filename = "";   // имя файла

            string docnum = "";    // Номер документа (акта, СФ, счета и тд)


            if (Document.Properties.Get("Отправлено") != "X" && Document.DefinitionName == "Постоплата Зарубеж")
            {
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

                for (int i = 0; i < Document.Field("Сводный раздел\\ResultTable").Rows.Count; i++)
                {
                    string dkz_number = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Belnr").Text;
                    string dkz_year = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Gjahr").Text;
                    string doc_num = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\DocNum").Text;
                    string ext_key = Document.Field("Сводный раздел\\ResultTable[" + i.ToString() + "]\\Ext_key").Text;

                    string[] req = new string[4];
                    string fullpath = Document.Batch.Project.EnvironmentVariables.Get("Export_path") + batchnum + "_" + setnum;
                    string[] filesname = Directory.GetFiles(fullpath);

                    // =========================================    
                    string str = "";
                    foreach (string file in filesname)
                    {
                        str += file;
                    }
                    Document.Properties.Set("Test2", str);
                    // =========================================   

                    //Send_File obj = new Send_File();

                    Dictionary<string, string> scans = new Dictionary<string, string>();

                    scans.Add("Attachment.pdf", "OTH");
                    scans.Add("Foreign_invoice" + doc_num + ".pdf", "6");
                    scans.Add("Reestr.pdf", "OTH");

                    int err = 0;    // переменная для проверки успешного экспорта документов
                    string err_mes = "";    // переменная для вывода ошибок

                    foreach (string filename in scans.Keys)
                    {
                        if (Array.IndexOf(filesname, fullpath + "\\" + filename) >= 0)
                        {
                            // ==============================================
                            // =================== ДКЗ ======================
                            // ==============================================

                            if (dkz_number != "")
                            {
                                string objkey = dkz_number + dkz_year;

                                Byte[] bytes = File.ReadAllBytes(fullpath + "\\" + filename);
                                req[0] = Convert.ToBase64String(bytes);
                                req[1] = scans[filename];
                                req[2] = objkey;
                                req[3] = "BUS2081";

                                SapExportModule.ExportDocument(req);
                                //err_mes = obj.InvokeService(req);
                                //if (err_mes != "Save OK")
                                //{
                                //    Processing.ReportWarning("Ошибка экспорта - " + err_mes);
                                //    err++;
                                //}
                            }

                            // ==============================================
                            // =================== КСч ======================
                            // ==============================================

                            if (ext_key != "" && filename.Contains("Attachment") == false && filename.Contains("Reestr") == false)
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
                                //err_mes = obj.InvokeService(req);
                                //if (err_mes != "Save OK")
                                //{
                                //    Processing.ReportWarning("Ошибка экспорта - " + err_mes);
                                //    err++;
                                //}
                            }
                        }
                    }

                    Document.Properties.Set("Отправлено", "X");

                    //if (err == 0)
                    //    Document.Properties.Set("Отправлено", "X");
                    //else
                    //    Processing.ReportError("Не все документы были прикреплены к документу в SAP ERP");
                }
            }









        }
    }
}
