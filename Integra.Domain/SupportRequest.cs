using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
//using FC12.SupportExtensions.Outlook;
//using Microsoft.Office.Interop.Outlook;



namespace Integra.Domain
{
    public class SupportRequest
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int SMID { get; set; }
        private readonly string _subject;
        public readonly string CreationDate;
        public string Subject
        {
            get { return $"{_subject} Приоритет: {(int)Priority}" ; }
        } 
        public Priority Priority { get; set; }
        public string Recipient { get; set; }
        public string CC { get; set; }
        //private AbbyyBatch Batch { get; set; }
        public string BatchId { get; set; }
        public string BatchName { get; set; }
        public string BatchOwner { get; set; }
        private CustomList<string> _categories { get; set; } = new CustomList<string>();

        public string Comment { get; set; }

        public bool IsValid()
        {
            bool result = _categories.Any();
            return result;
        }

        public void AddCategory(string category)
        {
            _categories.Add(category);
        }

        public void AddCategories(string categories)
        {
            var values = categories.Split(',');
            _categories.AddRange(values);
        }

        public void RemoveCategory(string category)
        {
            _categories.Remove(category);
        }

        public string Categories
        {
            get { return String.Join(", ", _categories); }
            set
            {
                var values = value.Split(',');
                _categories.AddRange(values);
            }
        }



        public SupportRequest()
        {
            CreationDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        }


    }

    public class CustomList<T> : List<T>
    {
        public new void Add(T item)
        {
            if (!base.Contains(item)) base.Add(item);
        }
    }

    public class AbbyyBatch
    {
        public string BatchId { get; set; }
        public string BatchName { get; set; }
        public string BatchOwner { get; set; }
    }

    
    public enum Priority : ushort
    {
        Low = 0, 
        Medium = 1, 
        High = 2,
        Urgent = 3,
    }


}
