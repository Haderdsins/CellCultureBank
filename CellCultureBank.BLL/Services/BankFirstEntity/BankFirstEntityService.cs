using AutoMapper;
using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Services.BankFirstEntity;

public class BankFirstEntityService : IBankFirstEntityService
{
    private readonly BankDbContext _dbContext;
    private readonly IMapper _firstBankMapper;
    public BankFirstEntityService(BankDbContext dbContext, IMapper firstBankMapper)
    {
        _dbContext = dbContext;
        _firstBankMapper = firstBankMapper;
    }

    public IEnumerable<BankFirst> GetAll()
    {
        return _dbContext.BankFirsts.ToList();
    }
    
    public void Create(CreateItemOfBankModel model)
    {
        var bankFirst = _firstBankMapper.Map<BankFirst>(model);
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

    public BankFirst Get(int BankId)
    {
        var bankItemToGet = _dbContext.BankFirsts.Find(BankId);
        if (bankItemToGet!=null)
        {
            return bankItemToGet ;
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }
    
    
    public void Update(int BankId, UpdateItemOfBankModel model)
    {
        var bankItemToUpdate = _dbContext.BankFirsts.Find(BankId);
        if (bankItemToUpdate!=null)
        {
            _firstBankMapper.Map(model, bankItemToUpdate);
            _dbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public void DeleteAll()
    {
        _dbContext.BankFirsts.RemoveRange(_dbContext.BankFirsts);
        _dbContext.SaveChanges();
        _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[BankFirsts]', RESEED, 0);");
    }

    public IEnumerable<BankFirst> GetSortedDescendingItemsOfBank()
    {
        return GetAll().OrderByDescending(p=>p.Date);
    }
    public IEnumerable<BankFirst> GetSortedItemsOfBank()
    {
        return GetAll().OrderBy(p=>p.Date);
    }

    public IEnumerable<BankFirst> GetAllOnDate(int year, int mounth, int day)
    {
        return GetAll()
            .Where(p => p.Date.Value.Year ==year &&  p.Date.Value.Month ==mounth && p.Date.Value.Day == day);
    }

    public IEnumerable<BankFirst> GetAllOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd)
    {
        return GetAll()
            .Where(p =>
                p.Date.Value.Year >= yearStart && p.Date.Value.Year <= yearEnd &&
                p.Date.Value.Month >= mounthStart && p.Date.Value.Month <= mounthEnd &&
                p.Date.Value.Day >= dayStart && p.Date.Value.Day <= dayEnd
            );
    }


    public int GetCountOfAllItems()
    {
        return GetAll().Count();
    }
}