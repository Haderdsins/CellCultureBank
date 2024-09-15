using AutoMapper;
using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Services.BankSecondEntity;

public class BankSecondEntityService : IBankSecondEntityService
{
    private readonly BankDbContext _dbSecondContext;
    private readonly IMapper _secondBankMapper;
    

    public BankSecondEntityService(BankDbContext dbContext, IMapper secondBankMapper)
    {
        _dbSecondContext = dbContext;
        _secondBankMapper = secondBankMapper;
    }
    
    public IEnumerable<BankSecond> GetAll()
    {
        return _dbSecondContext.BankSeconds.ToList();
    }
    
    public void Create(CreateItemOfSecondBank model)
    {
        var bankSecond = _secondBankMapper.Map<BankSecond>(model);

        _dbSecondContext.BankSeconds.Add(bankSecond);
        _dbSecondContext.SaveChanges();
    }

    public void Delete(int BankId)
    {
        var bankItemToDelete = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToDelete!=null)
        {
            _dbSecondContext.BankSeconds.Remove(bankItemToDelete);
            _dbSecondContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public BankSecond Get(int BankId)
    {
        var bankItemToGet = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToGet!=null)
        {
            return bankItemToGet ;
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }

    public void Update(int BankId, UpdateItemOfSecondBank model)
    {
        var bankItemToUpdate = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToUpdate!=null)
        {
            _secondBankMapper.Map(model, bankItemToUpdate);
            _dbSecondContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public void DeleteAll()
    {
        _dbSecondContext.BankSeconds.RemoveRange(_dbSecondContext.BankSeconds);
        _dbSecondContext.SaveChanges();
        _dbSecondContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[BankSeconds]', RESEED, 0);");
    }

    public IEnumerable<BankSecond> GetSortedDescendingItemsOfBank()
    {
        return GetAll().OrderByDescending(p=>p.DateOfDefrosting);
    }

    public IEnumerable<BankSecond> GetSortedItemsOfBank()
    {
        return GetAll().OrderBy(p=>p.DateOfDefrosting);
    }

    public IEnumerable<BankSecond> GetAllOnDateOfDefrosting(int year, int month, int day)
    {
        return GetAll()
            .Where(p => p.DateOfDefrosting != null && p.DateOfDefrosting.Value.Year ==year && p.DateOfDefrosting.Value.Month ==month && p.DateOfDefrosting.Value.Day == day);

    }

    public IEnumerable<BankSecond> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        return GetAll()
            .Where(p =>
                p.DateOfDefrosting != null &&
                p.DateOfDefrosting.Value.Year >= yearStart && p.DateOfDefrosting.Value.Year <= yearEnd &&
                p.DateOfDefrosting.Value.Month >= monthStart && p.DateOfDefrosting.Value.Month <= monthEnd &&
                p.DateOfDefrosting.Value.Day >= dayStart && p.DateOfDefrosting.Value.Day <= dayEnd
            );
    }

    public int GetCountOfAllItems()
    {
        return GetAll().Count();
    }
    
    public void UpdateBankCell(int id, UpdateCellModel model)
    {
        var bankCell = _dbSecondContext.BankSeconds.Find(id);
        
        if (bankCell == null)
        {
            throw new ArgumentException($"Клетка с ID {id} не найдена.");
        }

        // Обновляем свойства клетки
        bankCell.CellLine = model.CellLine;
        bankCell.Clearing = model.Clearing;
        bankCell.Certification = model.Certification;
        bankCell.Address = model.Address;
        bankCell.Quantity = model.Quantity;
        bankCell.Origin = model.Origin;

        // Сохраняем изменения
        _dbSecondContext.SaveChanges();
    }
}