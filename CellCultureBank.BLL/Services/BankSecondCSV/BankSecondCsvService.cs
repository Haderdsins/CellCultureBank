using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.DAL;
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
        var items = _dbSecondContext.BankOfCells
            .Select(bf => new BankCsvRecord
            {
                CellLine = bf.CellLine,
                Origin = bf.Origin,
                DateOfFreezing = bf.DateOfFreezing,
                FrozenByUserId = bf.FrozenByUserId,
                DateOfDefrosting = bf.DateOfDefrosting,
                DefrostedByUserId = bf.DefrostedByUserId,
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
            // Считывание данных из CSV в BankSecondCsvRecord
            var records = csv.GetRecords<BankCsvRecord>().ToList();

            // Преобразование записей в модели для БД, исключая ID
            var bankSeconds = records.Select(record => new BankOfCell()
            {
                // Не задаем поле ID, чтобы база данных его сгенерировала
                CellLine = record.CellLine ?? null,
                Origin = record.Origin ?? null,
                DateOfFreezing = record.DateOfFreezing ?? null,
                FrozenByUserId = record.FrozenByUserId ?? null,
                DateOfDefrosting = record.DateOfDefrosting ?? null,
                DefrostedByUserId = record.DefrostedByUserId ?? null,
                Clearing = record.Clearing,
                Certification = record.Certification,
                Quantity = record.Quantity ?? 0,
                Address = record.Address ?? null
            }).ToList();

            // Добавление данных в базу
            _dbSecondContext.BankOfCells.AddRange(bankSeconds);
            await _dbSecondContext.SaveChangesAsync();
        }
    }

}