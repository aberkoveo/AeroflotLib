using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Integra.Domain.Support;

public class SupportRequest
{
    private readonly string _subject;
    private CustomList<string> _categories { get; set; } = new CustomList<string>();
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public int SMID { get; set; }
    public readonly string CreationDate;
    [NotMapped]
    public string Subject => $"{_subject} Приоритет: {(int)Priority}";
    public Priority Priority { get; set; }
    [NotMapped]
    public string Recipient { get; set; }

    [NotMapped]
    public string CC { get; set; }

    public string BatchId { get; set; }
    [NotMapped]
    public string BatchName { get; set; }

    public string BatchOwner { get; set; }
    public string Comment { get; set; }
    [NotMapped]
    public bool IsValid => _categories.Any();

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
        get { return Join(", ", _categories); }
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
        if (!Contains(item)) base.Add(item);
    }
}


public enum Priority : ushort
{
    Low = 0,
    Medium = 1,
    High = 2,
    Urgent = 3,
}