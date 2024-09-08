using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using Microsoft.AspNetCore.Http;

namespace CellCultureBank.BLL.Services.BankFirst;

public interface IBankFirstService
{
    void Create(CreateItemOfBankModel model);
    
    void Delete(int BankId);
    
    DAL.Models.BankFirst Get(int BankId);

    IEnumerable<DAL.Models.BankFirst> GetAll(); 
    
    void Update(int BankId, UpdateItemOfBankModel model);

    void DeleteAll();

    IEnumerable<DAL.Models.BankFirst> GetSortedDescendingItemsOfBank();
    IEnumerable<DAL.Models.BankFirst> GetSortedItemsOfBank();

    int GetCountOfAllItems();

    Task<Stream> ExportToCsvAsync();
}