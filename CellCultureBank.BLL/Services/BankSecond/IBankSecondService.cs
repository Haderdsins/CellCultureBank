using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.BLL.Services.BankFirst;


namespace CellCultureBank.BLL.Services.BankSecond;

public interface IBankSecondService
{
    void Create(CreateItemOfSecondBank model);
    
    void Delete(int BankId);
    
    DAL.Models.BankSecond Get(int BankId);

    IEnumerable<DAL.Models.BankSecond> GetAll(); 
    
    void Update(int BankId, UpdateItemByIdOfSecondBank model);

    void DeleteAll();
    
    IEnumerable<DAL.Models.BankSecond> GetSortedDescendingItemsOfBank();
    
    IEnumerable<DAL.Models.BankSecond> GetSortedItemsOfBank();
    
    IEnumerable<DAL.Models.BankSecond> GetAllOnDateOfDefrosting(int year, int month, int day);
    
    IEnumerable<DAL.Models.BankSecond> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd);
    
    int GetCountOfAllItems();

    Task<Stream> ExportToCsvAsync();
    
    Task ImportFromCsvAsync(Stream csvStream);
}