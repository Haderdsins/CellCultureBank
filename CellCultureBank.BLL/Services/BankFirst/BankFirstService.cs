using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.BankFirst;

public class BankFirstService : IBankFirstService
{
    private readonly BankDbContext _dbContext;

    public BankFirstService(BankDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<DAL.Models.BankFirst> GetAllItems()
    {
        // Use Entity Framework to fetch all items from the database
        return _dbContext.BankFirsts.ToList();
    }
    
    public void Create(CreateItemOfBankModel model)
    {
        var bankFirst = new DAL.Models.BankFirst();
        bankFirst.Date = model.Date;
        bankFirst.Movement = model.Movement;
        bankFirst.Dewar = model.Dewar;
        bankFirst.Identifier = model.Identifier;
        bankFirst.NameOfCellCulture = model.NameOfCellCulture;
        bankFirst.Passage = model.Passage;
        bankFirst.QuantityOnLabel = model.QuantityOnLabel;
        bankFirst.Quantity = model.Quantity;
        bankFirst.ActualBalance = model.ActualBalance;
        bankFirst.FullName = model.FullName;
        bankFirst.Note = model.Note;

        _dbContext.BankFirsts.Add(bankFirst);
        _dbContext.SaveChanges();

    }

    public void Delete(int BankId)
    {
        var bankItemToDelete = _dbContext.BankFirsts.Find(BankId);
        if (bankItemToDelete!=null)
        {
            _dbContext.BankFirsts.Remove(bankItemToDelete);
            _dbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public DAL.Models.BankFirst Get(int BankId)
    {
        var bankItemToGet = _dbContext.BankFirsts.Find(BankId);
        if (bankItemToGet!=null)
        {
            return bankItemToGet ;
        }
        else
        {
            throw new ArgumentException("Клетки с таким id не найдено");
        }
    }

    public IEnumerable<DAL.Models.BankFirst> GetAll()
    {
        // Fetch all items from the bank repository
        return GetAllItems();
    }

    public void Update(int BankId, UpdateItemOfBankModel model)
    {
        var bankItemToUpdate = _dbContext.BankFirsts.Find(BankId);
        if (bankItemToUpdate!=null)
        {
            bankItemToUpdate.Date = model.Date;
            bankItemToUpdate.Movement = model.Movement;
            bankItemToUpdate.Dewar = model.Dewar;
            bankItemToUpdate.Identifier = model.Identifier;
            bankItemToUpdate.NameOfCellCulture = model.NameOfCellCulture;
            bankItemToUpdate.Passage = model.Passage;
            bankItemToUpdate.QuantityOnLabel = model.QuantityOnLabel;
            bankItemToUpdate.Quantity = model.Quantity;
            bankItemToUpdate.ActualBalance = model.ActualBalance;
            bankItemToUpdate.FullName = model.FullName;
            bankItemToUpdate.Note = model.Note;
            _dbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }
}