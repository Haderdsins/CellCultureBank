using AutoMapper;
using CellCultureBank.BLL.Models;
using CellCultureBank.DAL;
using CellCultureBank.DAL.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace CellCultureBank.BLL.Services.BankEntity;

public class BankEntityService : IBankEntityService
{
    private readonly BankDbContext _dbSecondContext;
    private readonly IMapper _secondBankMapper;

    public BankEntityService(BankDbContext dbContext, IMapper secondBankMapper)
    {
        _dbSecondContext = dbContext;
        _secondBankMapper = secondBankMapper;
    }

    public async Task Create(CreateItemModel model)
    {
        var bankSecond = _secondBankMapper.Map<BankOfCell>(model);
        await _dbSecondContext.BankOfCells.AddAsync(bankSecond);
        await _dbSecondContext.SaveChangesAsync();
    }

    public Task Delete(int bankId)
    {
        var bankItemToDelete = _dbSecondContext.BankOfCells.Find(bankId);
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

    public async Task<GetModel> Get(int bankId)
    {
        var bankItemToGet = await _dbSecondContext.BankOfCells.FindAsync(bankId);
        if (bankItemToGet != null)
        {
            return _secondBankMapper.Map<GetModel>(bankItemToGet);
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }


    public async Task<IEnumerable<GetModel>> GetAll()
    {
        var bankOfCells = await _dbSecondContext.BankOfCells.ToListAsync();
        return _secondBankMapper.Map<IEnumerable<GetModel>>(bankOfCells);
    }


    public async Task Update(int bankId, UpdateItemModel model)
    {
        var bankItemToUpdate = await _dbSecondContext.BankOfCells.FindAsync(bankId);
        if (bankItemToUpdate != null)
        {
            _secondBankMapper.Map(model, bankItemToUpdate);
            await _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException($"Клетка с id {bankId} не найдена");
        }
    }
    
    public async Task<IEnumerable<GetModel>> GetSortedDescendingItemsOfBank()
    {
        var sortedCells = await _dbSecondContext.BankOfCells
            .OrderByDescending(p => p.DateOfDefrosting)
            .ToListAsync();
    
        return _secondBankMapper.Map<IEnumerable<GetModel>>(sortedCells);
    }

    public async Task<IEnumerable<GetModel>> GetSortedItemsOfBank()
    {
        var sortedCells = await _dbSecondContext.BankOfCells
            .OrderBy(p => p.DateOfDefrosting)
            .ToListAsync();

        return _secondBankMapper.Map<IEnumerable<GetModel>>(sortedCells);
    }

    public async Task<IEnumerable<GetModel>> GetAllOnDateOfDefrosting(int year, int month, int day)
    {
        var bankOfCells = await _dbSecondContext.BankOfCells
            .Where(p => p.DateOfDefrosting.HasValue && 
                        p.DateOfDefrosting.Value.Year == year &&
                        p.DateOfDefrosting.Value.Month == month &&
                        p.DateOfDefrosting.Value.Day == day)
            .ToListAsync();
    
        return _secondBankMapper.Map<IEnumerable<GetModel>>(bankOfCells);
    }

    public async Task<IEnumerable<GetModel>> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        var bankOfCells = await _dbSecondContext.BankOfCells
            .Where(cell => cell.DateOfDefrosting.HasValue &&
                           cell.DateOfDefrosting.Value >= new DateTime(yearStart, monthStart, dayStart) &&
                           cell.DateOfDefrosting.Value <= new DateTime(yearEnd, monthEnd, dayEnd))
            .ToListAsync();

        return _secondBankMapper.Map<IEnumerable<GetModel>>(bankOfCells);
    }

    public async Task<IEnumerable<GetModel>> GetAllOnDateRangeOfFrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        var bankOfCells = await _dbSecondContext.BankOfCells
            .Where(cell => cell.DateOfFreezing.HasValue &&
                           cell.DateOfFreezing.Value >= new DateTime(yearStart, monthStart, dayStart) &&
                           cell.DateOfFreezing.Value <= new DateTime(yearEnd, monthEnd, dayEnd))
            .ToListAsync();

        return _secondBankMapper.Map<IEnumerable<GetModel>>(bankOfCells);
    }


    public async Task<int> GetCountOfAllItems()
    {
        return await _dbSecondContext.BankOfCells.CountAsync();
    }

    public async Task FreezeCell(int bankId, FreezeCellModel model)
    {
        var cellToFreeze = await _dbSecondContext.BankOfCells.FindAsync(bankId);
        if (cellToFreeze!=null)
        {
            _secondBankMapper.Map(model, cellToFreeze);
            await _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException($"Клетка с id {bankId} не найдена");
        }
    }

    public async Task DefrostCell(int bankId, DefrostCellModel model)
    {
        var cellToDefrost = await _dbSecondContext.BankOfCells.FindAsync(bankId);
        if (cellToDefrost!=null)
        {
            _secondBankMapper.Map(model, cellToDefrost);
            await _dbSecondContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException($"Клетка с id {bankId} не найдена");
        }
    }
}
