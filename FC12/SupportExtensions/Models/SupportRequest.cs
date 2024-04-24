using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using FC12.SupportExtensions.Outlook;
using Microsoft.Office.Interop.Outlook;


namespace FC12.SupportExtensions.Models
{
    public class SupportRequestDto
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int SMID { get; set; }

        public readonly string CreationDate;
        public string Subject
        {
            get { return $"{SupportData.Subject} Приоритет: {(int)RequestPriority}" ; }
        } 
        public RequestPriority RequestPriority { get; set; }
        public string Recipient { get; set; }
        public string CC { get; set; }
        //private AbbyyBatch Batch { get; set; }
        public string BatchId { get; set; }
        public string BatchName { get; set; }
        public string BatchOwner { get; set; }
        private CustomList<string> _categories { get; set; } = new CustomList<string>();

        public string Comment { get; set; } = "";

        public string DocumentsIds { get; set; } 

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



        public SupportRequestDto()
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

    
    public enum RequestPriority : ushort
    {
        Low = 4, 
        Medium = 3, 
        High = 2,
        Urgent = 1,
    }


}
