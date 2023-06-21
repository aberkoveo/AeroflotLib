using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroflotLib
{
    internal class DocumentSections
    {
        //Документы комлпекта
        public string[] SetDocumentsTypes = { "ТОРГ-12", "АктВыполненныхРабот", "Счет-фактура",
            "Счет на оплату", "УПД"};

        //Авансовый инвойс
        public string PrepaymentInvoice = "Авансовый инвойс";

        //Акт формы С
        public string ActFormC = "Акт формы С";

        //Иностранные инвойсы (счета)
        public string ForeignInvoice = "Инвойсы иностранные";

        //Реестр ЛУ 
        public string ReestrLU = "РеестрЛУ(Пасс)";

        //Иностранные реестры 
        public string[] ReestrTypes = { "Реестр услуг (Menzies)", "Реестр услуг (Swedavia)",
             "Реестр услуг (Transportstyrelsen)", "Реестр услуг (ICTS)",
             "Реестр услуг (LMC SERVICES B.V.)",  "Реестр услуг (Amsterdam Airport Schiphol)" };

        public string[] GetAllSections()
        {
            string[] result = SetDocumentsTypes.Concat(ReestrTypes)
                .Append(ActFormC)
                .Append(ForeignInvoice)
                .Append(PrepaymentInvoice)
                .Append(ReestrLU).ToArray();
            return result;
        }
    }
}
