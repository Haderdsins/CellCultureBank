namespace CellCultureBank.BLL.Services.BankFirstCSV;

public interface IBankFirstCsvService
{
    /// <summary>
    /// Экспорт в CSV
    /// </summary>
    /// <returns></returns>
    Task<Stream> ExportToCsvAsync();
    
    /// <summary>
    /// Импорт в CSV
    /// </summary>
    /// <param name="csvStream"></param>
    /// <returns></returns>
    Task ImportFromCsvAsync(Stream csvStream);
}