namespace CellCultureBank.BLL.Services.BankSecondCSV;

public interface IBankSecondCsvService
{
    Task<Stream> ExportToCsvAsync();
    
    Task ImportFromCsvAsync(Stream csvStream);
}