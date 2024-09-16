namespace CellCultureBank.BLL.Models.Search;
/// <summary>
/// Модель поиска клеток по дате
/// </summary>
public class SearchByDateModel
{
    public int Year { get; set; }//Год
    
    public int Mounth { get; set; }//Месяц
    
    public int Day { get; set; }//День
}