using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.BankFirstEntity;

public interface IBankFirstEntityService
{
    void Create(CreateItemOfBankModel model);
    
    void Delete(int BankId);
    
    BankFirst Get(int BankId);

    IEnumerable<BankFirst> GetAll(); 
    
    void Update(int BankId, UpdateItemOfBankModel model);

    void DeleteAll();
    
    IEnumerable<BankFirst> GetSortedDescendingItemsOfBank();
    
    IEnumerable<BankFirst> GetSortedItemsOfBank();
    
    IEnumerable<BankFirst> GetAllOnDate(int year, int mounth, int day);
    IEnumerable<BankFirst> GetAllOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd);
    
    int GetCountOfAllItems();
}