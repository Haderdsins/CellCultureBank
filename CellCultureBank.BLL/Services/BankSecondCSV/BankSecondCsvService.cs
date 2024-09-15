using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.BankSecond.CSV;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CellCultureBank.BLL.Services.BankSecondCSV;

public class BankSecondCsvService : IBankSecondCsvService
{
    private readonly BankDbContext _dbSecondContext;

    public BankSecondCsvService(BankDbContext dbSecondContext)
    {
        _dbSecondContext = dbSecondContext;
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
            var bankSeconds = records.Select(record => new BankSecond()
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
}