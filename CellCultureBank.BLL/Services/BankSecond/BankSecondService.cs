using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.CSV;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Database;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CellCultureBank.BLL.Services.BankSecond;

public class BankSecondService : IBankSecondService
{
    private readonly BankDbContext _dbSecondContext;

    public BankSecondService(BankDbContext dbContext)
    {
        _dbSecondContext = dbContext;
    }
    
    public List<DAL.Models.BankSecond> GetAllItems()
    {
        return _dbSecondContext.BankSeconds.ToList();
    }
    
    public void Create(CreateItemOfSecondBank model)
    {
        var bankSecond = new DAL.Models.BankSecond();
        bankSecond.CellLine = model.CellLine;
        bankSecond.Origin = model.Origin;
        bankSecond.DateOfFreezing = model.DateOfFreezing;
        bankSecond.FrozenByFullName = model.FrozenByFullName;
        bankSecond.DateOfDefrosting = model.DateOfDefrosting;
        bankSecond.DefrostedByFullName = model.DefrostedByFullName;
        bankSecond.Clearing = model.Clearing;
        bankSecond.Certification = model.Certification;
        bankSecond.Quantity = model.Quantity;
        bankSecond.Address = model.Address;

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

    public DAL.Models.BankSecond Get(int BankId)
    {
        var bankItemToGet = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToGet!=null)
        {
            return bankItemToGet ;
        }
        throw new ArgumentException("Клетки с таким id не найдено");
    }

    public IEnumerable<DAL.Models.BankSecond> GetAll()
    {
        return GetAllItems();
    }

    public void Update(int BankId, UpdateItemByIdOfSecondBank model)
    {
        var bankItemToUpdate = _dbSecondContext.BankSeconds.Find(BankId);
        if (bankItemToUpdate!=null)
        {
            bankItemToUpdate.CellLine = model.CellLine;
            bankItemToUpdate.Origin = model.Origin;
            bankItemToUpdate.DateOfFreezing = model.DateOfFreezing;
            bankItemToUpdate.FrozenByFullName = model.FrozenByFullName;
            bankItemToUpdate.DateOfDefrosting = model.DateOfDefrosting;
            bankItemToUpdate.DefrostedByFullName = model.DefrostedByFullName;
            bankItemToUpdate.Clearing = model.Clearing;
            bankItemToUpdate.Certification = model.Certification;
            bankItemToUpdate.Quantity = model.Quantity;
            bankItemToUpdate.Address = model.Address;
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

    public IEnumerable<DAL.Models.BankSecond> GetSortedDescendingItemsOfBank()
    {
        return GetAllItems().OrderByDescending(p=>p.DateOfDefrosting);
    }

    public IEnumerable<DAL.Models.BankSecond> GetSortedItemsOfBank()
    {
        return GetAllItems().OrderBy(p=>p.DateOfDefrosting);
    }

    public IEnumerable<DAL.Models.BankSecond> GetAllOnDateOfDefrosting(int year, int month, int day)
    {
        return GetAllItems()
            .Where(p => p.DateOfDefrosting != null && p.DateOfDefrosting.Value.Year ==year && p.DateOfDefrosting.Value.Month ==month && p.DateOfDefrosting.Value.Day == day);

    }

    public IEnumerable<DAL.Models.BankSecond> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        return GetAllItems()
            .Where(p =>
                p.DateOfDefrosting != null &&
                p.DateOfDefrosting.Value.Year >= yearStart && p.DateOfDefrosting.Value.Year <= yearEnd &&
                p.DateOfDefrosting.Value.Month >= monthStart && p.DateOfDefrosting.Value.Month <= monthEnd &&
                p.DateOfDefrosting.Value.Day >= dayStart && p.DateOfDefrosting.Value.Day <= dayEnd
            );
    }

    public int GetCountOfAllItems()
    {
        return GetAllItems().Count;
    }

    public async Task<Stream> ExportToCsvAsync()
    {
        var items = _dbSecondContext.BankSeconds
            .Select(bf => new BankSecondCsvRecord
            {
                CellLine = bf.CellLine,
                Origin = bf.Origin,
                DateOfFreezing = bf.DateOfFreezing,
                FrozenByFullName = bf.FrozenByFullName,
                DateOfDefrosting = bf.DateOfDefrosting,
                DefrostedByFullName = bf.DefrostedByFullName,
                Clearing = bf.Clearing,
                Certification = bf.Certification,
                Quantity = bf.Quantity,
                Address = bf.Address,
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
            var records = csv.GetRecords<BankSecondCsvRecord>().ToList();

            // Преобразование записей в модели для БД
            var bankSeconds = records.Select(record => new DAL.Models.BankSecond()
            {
                CellLine = record.CellLine?? null,
                Origin = record.Origin?? null,
                DateOfFreezing = record.DateOfFreezing?? null,
                FrozenByFullName = record.FrozenByFullName?? null,
                DateOfDefrosting = record.DateOfDefrosting?? null,
                DefrostedByFullName = record.DefrostedByFullName?? null,
                Clearing = record.Clearing,
                Certification = record.Certification,
                Quantity = record.Quantity?? 0,
                Address = record.Address?? null,
            }).ToList();

            // Добавление данных в базу
            _dbSecondContext.BankSeconds.AddRange(bankSeconds);
            await _dbSecondContext.SaveChangesAsync();
        }
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