namespace CellCultureBank.DAL.Models;

public class BankSecond : Entity
{
    public int Id{ get; set; }//Id в таблице
    
    public String CellLine{ get; set; }//Клеточная линия
    
    public String Origin{ get; set; }//Происхождение
    
    public DateTime? DateOfFreezing{ get; set; }//Дата заморозки
    
    public String FrozenByFullName{ get; set; }//Кем заморожена
    
    public DateTime? DateOfDefrosting{ get; set; }//Дата разморозки
    
    public String DefrostedByFullName{ get; set; }//Кем разморожена
    
    public bool? Clearing{ get; set; }//Отчистка
    
    public bool? Certification{ get; set; }//Паспортизация
    
    public String Address{ get; set; }//Адрес
    
    public int? Quantity{ get; set; }//Количество
}