namespace CellCultureBank.BLL.Services.BankFirstCSV;

public interface IBankFirstCsvService
{
    Task<Stream> ExportToCsvAsync();
    
    Task ImportFromCsvAsync(Stream csvStream);
}