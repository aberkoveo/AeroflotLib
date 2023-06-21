using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC12.SupportExtensions.Outlook
{
    public static class Mail
    {
        public static string Subject { get; } = "Запрос в техподдержку;";

        public static string[] Body = new string[]
        {
            "<br>Категории: {0}</br>",
            "<br></br>",
            "<br>Идентификатор пакета: {1}</br>",
            "<br></br>",
            "<br>Имя пакета: {2}</br>",
            "<br></br>",
            "<br>Комментарий:</br>",
            "<br>{3}</br>"
        };

        
    }
}


