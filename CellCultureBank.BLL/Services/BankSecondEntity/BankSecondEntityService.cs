using AutoMapper;
using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public Task Create(CreateItemOfSecondBank model)
    {
        var bankSecond = _secondBankMapper.Map<BankSecond>(model);

        _dbSecondContext.BankSeconds.Add(bankSecond);
        return _dbSecondContext.SaveChangesAsync();
    }

    public Task Delete(int BankId)
    {
        var bankItemToDelete = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToDelete != null)
        {
            _dbSecondContext.BankSeconds.Remove(bankItemToDelete);
            return _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public async Task<BankSecond> Get(int BankId)
    {
        var bankItemToGet = await _dbSecondContext.BankSeconds.FindAsync(BankId);
        if (bankItemToGet != null)
        {
            return bankItemToGet;
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }

    public async Task<IEnumerable<BankSecond>> GetAll()
    {
        return await _dbSecondContext.BankSeconds.ToListAsync();
    }

    public Task Update(int BankId, UpdateItemOfSecondBank model)
    {
        var bankItemToUpdate = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToUpdate != null)
        {
            _secondBankMapper.Map(model, bankItemToUpdate);
            return _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public async Task DeleteAll()
    {
        _dbSecondContext.BankSeconds.RemoveRange(_dbSecondContext.BankSeconds);
        await _dbSecondContext.SaveChangesAsync();
    
        await _dbSecondContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[BankSeconds]', RESEED, 0)");
    }


    public async Task<IEnumerable<BankSecond>> GetSortedDescendingItemsOfBank()
    {
        return await _dbSecondContext.BankSeconds.OrderByDescending(p => p.DateOfDefrosting).ToListAsync();
    }

    public async Task<IEnumerable<BankSecond>> GetSortedItemsOfBank()
    {
        return await _dbSecondContext.BankSeconds.OrderBy(p => p.DateOfDefrosting).ToListAsync();
    }

    public async Task<IEnumerable<BankSecond>> GetAllOnDateOfDefrosting(int year, int month, int day)
    {
        return await _dbSecondContext.BankSeconds
            .Where(p => p.DateOfDefrosting.HasValue && 
                        p.DateOfDefrosting.Value.Year == year &&
                        p.DateOfDefrosting.Value.Month == month &&
                        p.DateOfDefrosting.Value.Day == day)
            .ToListAsync();
    }

    public async Task<IEnumerable<BankSecond>> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        return await _dbSecondContext.BankSeconds
            .Where(p => p.DateOfDefrosting.HasValue &&
                        p.DateOfDefrosting.Value.Year >= yearStart &&
                        p.DateOfDefrosting.Value.Year <= yearEnd &&
                        p.DateOfDefrosting.Value.Month >= monthStart &&
                        p.DateOfDefrosting.Value.Month <= monthEnd &&
                        p.DateOfDefrosting.Value.Day >= dayStart &&
                        p.DateOfDefrosting.Value.Day <= dayEnd)
            .ToListAsync();
    }

    public async Task<int> GetCountOfAllItems()
    {
        return await _dbSecondContext.BankSeconds.CountAsync();
    }

    public async Task UpdateBankCell(int id, UpdateCellModel model)
    {
        var bankCell = await _dbSecondContext.BankSeconds.FindAsync(id);
        
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
        await _dbSecondContext.SaveChangesAsync();
    }
}
