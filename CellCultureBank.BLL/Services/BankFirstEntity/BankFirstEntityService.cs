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

    public async Task<IEnumerable<BankFirst>> GetAll()
    {
        return await _dbContext.BankFirsts.ToListAsync();
    }
    
    public async Task Create(CreateItemOfBankModel model)
    {
        try
        {
            var bankFirst = _firstBankMapper.Map<BankFirst>(model);
            _dbContext.BankFirsts.Add(bankFirst);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task Delete(int BankId)
    {
        var bankItemToDelete = await _dbContext.BankFirsts.FindAsync(BankId);
        if (bankItemToDelete != null)
        {
            _dbContext.BankFirsts.Remove(bankItemToDelete);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена", nameof(BankId));
        }
    }

    public async Task<BankFirst> Get(int BankId)
    {
        var bankItemToGet = await _dbContext.BankFirsts.FindAsync(BankId);
        if (bankItemToGet != null)
        {
            return bankItemToGet;
        }
        throw new ArgumentException("Клетки с таким id не найдено", nameof(BankId));
    }
    
    
    public async Task Update(int BankId, UpdateItemOfBankModel model)
    {
        var bankItemToUpdate = await _dbContext.BankFirsts.FindAsync(BankId);
        if (bankItemToUpdate != null)
        {
            _firstBankMapper.Map(model, bankItemToUpdate);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена", nameof(BankId));
        }
    }

    public async Task DeleteAll()
    {
        _dbContext.BankFirsts.RemoveRange(_dbContext.BankFirsts);
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[BankFirsts]', RESEED, 0);");
    }

    public async Task<IEnumerable<BankFirst>> GetSortedDescendingItemsOfBank()
    {
        return await _dbContext.BankFirsts.OrderByDescending(p => p.Date).ToListAsync();
    }
    public async Task<IEnumerable<BankFirst>> GetSortedItemsOfBank()
    {
        return await _dbContext.BankFirsts.OrderBy(p => p.Date).ToListAsync();
    }

    public async Task<IEnumerable<BankFirst>> GetAllOnDate(int year, int mounth, int day)
    {
        return await _dbContext.BankFirsts
            .Where(p => p.Date.Value.Year == year && p.Date.Value.Month == mounth && p.Date.Value.Day == day)
            .ToListAsync();
    }

    public async Task<IEnumerable<BankFirst>> GetAllOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd)
    {
        return await _dbContext.BankFirsts
            .Where(p =>
                p.Date.Value.Year >= yearStart && p.Date.Value.Year <= yearEnd &&
                p.Date.Value.Month >= mounthStart && p.Date.Value.Month <= mounthEnd &&
                p.Date.Value.Day >= dayStart && p.Date.Value.Day <= dayEnd)
            .ToListAsync();
    }


    public async Task<int> GetCountOfAllItems()
    {
        return await _dbContext.BankFirsts.CountAsync();
    }
}