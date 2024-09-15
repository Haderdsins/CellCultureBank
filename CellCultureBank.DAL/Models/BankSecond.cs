namespace CellCultureBank.DAL.Models;

public class BankSecond : Entity<int>
{
    /// <summary>
    /// Клеточная линия
    /// </summary>
    public string CellLine { get; set; } = null!;
    
    /// <summary>
    /// Происхождение
    /// </summary>
    public string Origin{ get; set; }= null!;
    
    /// <summary>
    /// Дата заморозки
    /// </summary>
    public DateTime? DateOfFreezing{ get; set; }
    
    /// <summary>
    /// Кем заморожена
    /// </summary>
    public string? FrozenByFullName{ get; set; }
    
    /// <summary>
    /// Дата разморозки
    /// </summary>
    public DateTime? DateOfDefrosting{ get; set; }
    
    /// <summary>
    /// Кем разморожена
    /// </summary>
    public string? DefrostedByFullName{ get; set; }
    
    /// <summary>
    /// Отчистка
    /// </summary>
    public bool Clearing{ get; set; }
    
    /// <summary>
    /// Паспортизация
    /// </summary>
    public bool Certification{ get; set; }
    
    /// <summary>
    /// Адрес
    /// </summary>
    public string Address{ get; set; }= null!;
    
    /// <summary>
    /// Количество
    /// </summary>
    public int? Quantity{ get; set; }
}