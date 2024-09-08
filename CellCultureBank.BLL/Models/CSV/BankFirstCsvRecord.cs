using CsvHelper.Configuration.Attributes;

namespace CellCultureBank.BLL.Models.CSV;

public class BankFirstCsvRecord
{
    [Name("Дата")]
    public DateTime Date { get; set; }

    [Name("Движение")]
    public String Movement { get; set; }

    [Name("Дьюар")]
    public String Dewar { get; set; }

    [Name("Идентификатор")]
    public int Identifier { get; set; }

    [Name("Название культуры клеток")]
    public String NameOfCellCulture { get; set; }

    [Name("Проход")]
    public String Passage { get; set; }

    [Name("Количество на этикетке")]
    public int QuantityOnLabel { get; set; }

    [Name("Количество")]
    public int Quantity { get; set; }

    [Name("Актуальный баланс")]
    public int ActualBalance { get; set; }

    [Name("Ф.И.О.")]
    public string FullName { get; set; }

    [Name("Примечание")]
    public string Note { get; set; }
}