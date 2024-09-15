using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.CSV;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CellCultureBank.BLL.Services.BankFirstCSV;

public class BankFirstCsvService : IBankFirstCsvService
{
    private readonly BankDbContext _dbContext;

    public BankFirstCsvService(BankDbContext dbContext)
    {
        _dbContext = dbContext;
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
            var bankFirsts = records.Select(record => new BankFirst
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