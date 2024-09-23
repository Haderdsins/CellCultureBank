namespace CellCultureBank.BLL.Models;
/// <summary>
/// Модель обновления клетки
/// </summary>
public class UpdateItemModel
{
    /// <summary>
    /// Клеточная линия
    /// </summary>
    public string CellLine { get; set; } = null!;
    
    /// <summary>
    /// Происхождение
    /// </summary>
    public string Origin { get; set; } = null!;
    
    /// <summary>
    /// Отчистка
    /// </summary>
    public bool Clearing { get; set; }
    
    /// <summary>
    /// Паспортизация
    /// </summary>
    public bool Certification { get; set; }
    
    /// <summary>
    /// Адрес
    /// </summary>
    public string Address { get; set; } = null!;
    
    /// <summary>
    /// Количество
    /// </summary>
    public int? Quantity { get; set; }
}