namespace CellCultureBank.DAL.Models;

public class BankFirst : Entity
{
    public int Id { get; set; }//Id в таблице
    
    public DateTime? Date { get; set; }//Дата

    public String Movement { get; set; }//Движение
    
    public String Dewar { get; set; }//Дьюар
    
    public int Identifier { get; set; } //Общий идентификатор
    
    public String NameOfCellCulture { get; set; }//Название культуры клеток

    public String Passage { get; set; }//Пассаж

    public int QuantityOnLabel { get; set; }//Количество на этикетке
    
    public int Quantity { get; set; }//Количество
    
    public int ActualBalance { get; set; }//Фактический остаток

    public String FullName { get; set; }//ФИО
    
    public String Note { get; set; }//Примечание
}