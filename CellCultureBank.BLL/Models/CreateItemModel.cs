namespace CellCultureBank.BLL.Models;
/// <summary>
/// Модель создания клетки для второго банка
/// </summary>
public class CreateItemModel
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
    /// Дата заморозки
    /// </summary>
    public DateTime? DateOfFreezing { get; set; }
    
    /// <summary>
    /// Id пользователя кто заморозил
    /// </summary>
    public int? FrozenByUserId { get; set; }
    
    /// <summary>
    /// Дата разморозки
    /// </summary>
    public DateTime? DateOfDefrosting { get; set; }
    
    /// <summary>
    /// Id пользователя кто разморозил
    /// </summary>
    public int? DefrostedByUserId { get; set; }
    
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