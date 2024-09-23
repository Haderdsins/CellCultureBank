namespace CellCultureBank.BLL.Models;
/// <summary>
/// Модель разморозки клетки
/// </summary>
public class DefrostCellModel
{
    /// <summary>
    /// Id кем разморожена
    /// </summary>
    public int? DefrostedByUserId { get; set; }
    
    /// <summary>
    /// Дата разморозки
    /// </summary>
    public DateTime? DateOfDefrosting { get; set; }
    
}