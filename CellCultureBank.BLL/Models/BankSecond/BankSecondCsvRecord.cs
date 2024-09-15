using CsvHelper.Configuration.Attributes;

namespace CellCultureBank.BLL.Models.BankSecond.CSV;

public class BankSecondCsvRecord
{
    
    [Name("Клеточная линия")]
    public String? CellLine{ get; set; }//Клеточная линия
    
    [Name("Происхождение")]
    public String? Origin{ get; set; }//Происхождение
    
    [Name("Дата заморозки")]
    [Format("dd/MM/yyyy HH:mm:ss")]
    public DateTime? DateOfFreezing{ get; set; }//Дата заморозки
    
    [Name("Кем заморожена")]
    public String? FrozenByFullName{ get; set; }//Кем заморожена
    
    [Name("Дата разморозки")]
    [Format("dd/MM/yyyy HH:mm:ss")]
    public DateTime? DateOfDefrosting{ get; set; }//Дата разморозки
    
    [Name("Кем разморожена")]
    public String? DefrostedByFullName{ get; set; }//Кем разморожена
    
    [Name("Отчистка")]
    public bool Clearing{ get; set; }//Отчистка
    
    [Name("Паспортизация")]
    public bool Certification{ get; set; }//Паспортизация
    
    [Name("Адрес")]
    public String? Address{ get; set; }//Адрес
    
    [Name("Количество")]
    public int? Quantity{ get; set; }//Количество
}