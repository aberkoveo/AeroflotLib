using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC12.SupportExtensions.Outlook
{
    public static class SupportData
    {
        public static string Subject = "Запрос в техподдержку ПСК;";

        public static string[] Body = new string[]
        {
            "<br>Категории: {0}</br>",
            "<br></br>",
            "<br>Идентификатор пакета: {1}</br>",
            "<br></br>",
            "<br>Идентификаторы документов: {2}</br>",
            "<br></br>",
            "<br>Имя пакета: {3}</br>",
            "<br></br>",
            "<br>Комментарий:</br>",
            "<br>{4}</br>",
            "<br></br>",
            "<br>Инцидент Solution Manager:</br>",
            "<br>{5}</br>"
        };

        
    }
}


