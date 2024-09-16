namespace CellCultureBank.BLL.Services.BankSecondCSV;

public interface IBankSecondCsvService
{
    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    /// <returns></returns>
    Task<Stream> ExportToCsvAsync();
    
    /// <summary>
    /// Импорт данных из CSV
    /// </summary>
    /// <param name="csvStream"></param>
    /// <returns></returns>
    Task ImportFromCsvAsync(Stream csvStream);
}