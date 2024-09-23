using AutoMapper;
using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.DAL;
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

    public Task Create(CreateItemOfBank model)
    {
        var bankSecond = _secondBankMapper.Map<BankOfCell>(model);

        _dbSecondContext.BankOfCells.Add(bankSecond);
        return _dbSecondContext.SaveChangesAsync();
    }

    public Task Delete(int BankId)
    {
        var bankItemToDelete = _dbSecondContext.BankOfCells.Find(BankId);
        if (bankItemToDelete != null)
        {
            _dbSecondContext.BankOfCells.Remove(bankItemToDelete);
            return _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Клетка с таким id не найдена");
        }
    }

    public async Task<BankOfCell> Get(int BankId)
    {
        var bankItemToGet = await _dbSecondContext.BankOfCells.FindAsync(BankId);
        if (bankItemToGet != null)
        {
            return bankItemToGet;
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }

    public async Task<IEnumerable<BankOfCell>> GetAll()
    {
        return await _dbSecondContext.BankOfCells
            .Include(b => b.FrozenByUser) // Загружаем пользователя, который заморозил клетку
            .Include(b => b.DefrostedByUser) // Загружаем пользователя, который разморозил клетку
            .ToListAsync();
    }

    public Task Update(int BankId, UpdateItemOfBank model)
    {
        var bankItemToUpdate = _dbSecondContext.BankOfCells.Find(BankId);
        if (bankItemToUpdate != null)
        {
            _secondBankMapper.Map(model, bankItemToUpdate);
            return _dbSecondContext.SaveChangesAsync();
        }
        throw new ArgumentException("Клетка с таким id не найдена");
        
    }

    public async Task DeleteAll()
    {
        _dbSecondContext.BankOfCells.RemoveRange(_dbSecondContext.BankOfCells);
        await _dbSecondContext.SaveChangesAsync();
    
        await _dbSecondContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[BankSeconds]', RESEED, 0)");
    }


    public async Task<IEnumerable<BankOfCell>> GetSortedDescendingItemsOfBank()
    {
        return await _dbSecondContext.BankOfCells.OrderByDescending(p => p.DateOfDefrosting).ToListAsync();
    }

    public async Task<IEnumerable<BankOfCell>> GetSortedItemsOfBank()
    {
        return await _dbSecondContext.BankOfCells.OrderBy(p => p.DateOfDefrosting).ToListAsync();
    }

    public async Task<IEnumerable<BankOfCell>> GetAllOnDateOfDefrosting(int year, int month, int day)
    {
        return await _dbSecondContext.BankOfCells
            .Where(p => p.DateOfDefrosting.HasValue && 
                        p.DateOfDefrosting.Value.Year == year &&
                        p.DateOfDefrosting.Value.Month == month &&
                        p.DateOfDefrosting.Value.Day == day)
            .ToListAsync();
    }

    public async Task<IEnumerable<BankOfCell>> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        return await _dbSecondContext.BankOfCells
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
        return await _dbSecondContext.BankOfCells.CountAsync();
    }

    public async Task UpdateBankCell(int id, UpdateCellModel model)
    {
        var bankCell = await _dbSecondContext.BankOfCells.FindAsync(id);
        
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
