using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.CSV;
using CellCultureBank.BLL.Models.Get;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Services.BankFirst;

public class BankFirstService : IBankFirstService
{
    private readonly BankDbContext _dbContext;
    //TODO: добавить реализацию вывода всех клеток по определнному диапозону дат, поиск элементов по тексту
    //TODO: импорт данных в CSV и EXCEL
    public BankFirstService(BankDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<DAL.Models.BankFirst> GetAllItems()
    {
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
        throw new ArgumentException("Клетки с таким id не найдено");
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

    public void DeleteAll()
    {
        _dbContext.BankFirsts.RemoveRange(_dbContext.BankFirsts);
        _dbContext.SaveChanges();
        _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[BankFirsts]', RESEED, 0);");
    }

    public IEnumerable<DAL.Models.BankFirst> GetSortedDescendingItemsOfBank()
    {
        return GetAllItems().OrderByDescending(p=>p.Date);
    }
    public IEnumerable<DAL.Models.BankFirst> GetSortedItemsOfBank()
    {
        return GetAllItems().OrderBy(p=>p.Date);
    }

    public int GetCountOfAllItems()
    {
        return GetAllItems().Count();
    }

    public async Task<Stream> ExportToCsvAsync()
    {
        var items = _dbContext.BankFirsts
            .Select(bf => new BankFirstCsvRecord
            {
                Date = bf.Date,
                Movement = bf.Movement,
                Dewar = bf.Dewar,
                Identifier = bf.Identifier,
                NameOfCellCulture = bf.NameOfCellCulture,
                Passage = bf.Passage,
                QuantityOnLabel = bf.QuantityOnLabel,
                Quantity = bf.Quantity,
                ActualBalance = bf.ActualBalance,
                FullName = bf.FullName,
                Note = bf.Note
            })
            .ToList();

        if (items == null || !items.Any())
        {
            return null;
        }

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
        };

        var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true))
        using (var csv = new CsvWriter(writer, csvConfig))
        {
            csv.WriteRecords(items);
            await writer.FlushAsync();
            stream.Position = 0; // Reset stream position to the beginning
        }

        return stream;
    }

    public async Task ImportFromCsvAsync(Stream csvStream)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
        };

        using (var reader = new StreamReader(csvStream))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            // Считывание данных из CSV в BankFirstCsvRecord
            var records = csv.GetRecords<BankFirstCsvRecord>().ToList();

            // Преобразование записей в модели для БД
            var bankFirsts = records.Select(record => new DAL.Models.BankFirst
            {
                Date = record.Date?? null,
                Movement = record.Movement?? null,
                Dewar = record.Dewar?? null,
                Identifier = record.Identifier,
                NameOfCellCulture = record.NameOfCellCulture?? null,
                Passage = record.Passage?? null,
                QuantityOnLabel = record.QuantityOnLabel?? 0,
                Quantity = record.Quantity?? 0,
                ActualBalance = record.ActualBalance?? 0,
                FullName = record.FullName?? null,
                Note = record.Note?? null
            }).ToList();

            // Добавление данных в базу
            _dbContext.BankFirsts.AddRange(bankFirsts);
            await _dbContext.SaveChangesAsync();
        }
    }
}