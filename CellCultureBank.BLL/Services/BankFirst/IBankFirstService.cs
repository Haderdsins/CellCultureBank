using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;

namespace CellCultureBank.BLL.Services.BankFirst;

public interface IBankFirstService
{
    void Create(CreateItemOfBankModel model);
    
    void Delete(int BankId);
    
    DAL.Models.BankFirst Get(int BankId);

    IEnumerable<DAL.Models.BankFirst> GetAll(); 
    
    void Update(int BankId, UpdateItemOfBankModel model);
}