using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.BankSecondEntity;

public interface IBankSecondEntityService
{
    void Create(CreateItemOfSecondBank model);
    
    void Delete(int BankId);
    
    BankSecond Get(int BankId);

    IEnumerable<BankSecond> GetAll(); 
    
    void Update(int BankId, UpdateItemOfSecondBank model);

    void DeleteAll();
    
    IEnumerable<BankSecond> GetSortedDescendingItemsOfBank();
    
    IEnumerable<BankSecond> GetSortedItemsOfBank();
    
    IEnumerable<BankSecond> GetAllOnDateOfDefrosting(int year, int month, int day);
    
    IEnumerable<BankSecond> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd);
    
    int GetCountOfAllItems();
    
    void UpdateBankCell(int id, UpdateCellModel model);
}