namespace CellCultureBank.DAL.Models;

public class BankFirst : Entity<int>
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime? Date { get; set; }
    
    /// <summary>
    /// Движение
    /// </summary>
    public string Movement { get; set; }= null!;
    
    /// <summary>
    /// Дьюар
    /// </summary>
    public string Dewar { get; set; }= null!;
    
    /// <summary>
    /// Общий идентификатор
    /// </summary>
    public int Identifier { get; set; }
    
    /// <summary>
    /// Название культуры клеток
    /// </summary>
    public string NameOfCellCulture { get; set; }
    
    /// <summary>
    /// Пассаж
    /// </summary>
    public string Passage { get; set; }= null!;
    
    /// <summary>
    /// Количество на этикетке
    /// </summary>
    public int QuantityOnLabel { get; set; }
    
    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Фактический остаток
    /// </summary>
    public int ActualBalance { get; set; }
    
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; }= null!;
    
    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }
}