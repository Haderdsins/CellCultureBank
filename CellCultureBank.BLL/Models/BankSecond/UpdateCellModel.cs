namespace CellCultureBank.BLL.Models.BankSecond;
/// <summary>
/// Модель обновления клетки второго банка, без дат разморозки и заморозки
/// </summary>
public class UpdateCellModel
{
    public int Id { get; set; }
    public String CellLine{ get; set; }//Клеточная линия
    
    public String Origin{ get; set; }//Происхождение
    
    public bool Clearing{ get; set; }//Отчистка
    
    public bool Certification{ get; set; }//Паспортизация
    
    public String Address{ get; set; }//Адрес
    
    public int? Quantity{ get; set; }//Количество
}