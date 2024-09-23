using Microsoft.AspNetCore.Mvc.Rendering;

namespace CellCultureBank.BLL.Models.BankSecond;
/// <summary>
/// Модель создания клетки для второго банка
/// </summary>
public class CreateItemOfBank
{
    public string CellLine { get; set; } = null!;
    public string Origin { get; set; } = null!;
    public DateTime? DateOfFreezing { get; set; }
    
    public int? FrozenByUserId { get; set; }
    public DateTime? DateOfDefrosting { get; set; }
    
    public int? DefrostedByUserId { get; set; }
    public bool Clearing { get; set; }
    public bool Certification { get; set; }
    public string Address { get; set; } = null!;
    public int? Quantity { get; set; }
    
    public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
}