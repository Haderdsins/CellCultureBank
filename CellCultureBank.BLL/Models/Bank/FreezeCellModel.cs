namespace CellCultureBank.BLL.Models;
/// <summary>
/// Модель заморозки клетки
/// </summary>
public class FreezeCellModel
{
    /// <summary>
    /// Id кем заморожена
    /// </summary>
    public int? FrozenByUserId { get; set; }
    
    /// <summary>
    /// Дата заморозки
    /// </summary>
    public DateTime? DateOfFreezing { get; set; }
}