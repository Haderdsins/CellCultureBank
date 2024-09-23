using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.BLL.Services.BankCSV;
using CellCultureBank.BLL.Services.BankEntity;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BankController : ControllerBase
{
    private readonly IBankEntityService _bankEntityService;
    private readonly IBankCsvService _bankCsvService;

    public BankController(IBankEntityService bankEntityService, IBankCsvService bankCsvService)
    {
        _bankEntityService = bankEntityService;
        _bankCsvService = bankCsvService;
    }

    /// <summary>
    /// Создать клетку
    /// </summary>
    /// <param name="createItemOfBank"></param>
    [HttpPost("CreateItemOfSecondBank")]
    public async Task<IActionResult> CreateItemOfSecondBank(CreateItemOfBank createItemOfBank)
    {
        await _bankEntityService.Create(createItemOfBank);
        return Ok("Клетка успешно создана");
    }
    
    /// <summary>
    /// Удалить клетку по id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("CreateItemOfSecondBank")]
    public async Task<IActionResult> DeleteItemOfSecondBank(int id)
    {
        await _bankEntityService.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public async Task<IActionResult> GetItemOfBank(int BankId)
    {
        var itemOfBank = await _bankEntityService.Get(BankId);
        return Ok(itemOfBank);
    }

    /// <summary>
    /// Получить все клетки
    /// </summary>
    [HttpGet("GetAllItemOfBank")]
    public async Task<IActionResult> GetAllItemOfBank()
    {
        var allItems = await _bankEntityService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public async Task<IActionResult> GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = await _bankEntityService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    /// Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    [HttpGet("GetSortedItemsOfBank")]
    public async Task<IActionResult> GetSortedItemsOfBank()
    {
        var allSortItems = await _bankEntityService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }

    /// <summary>
    /// Получить количество записей
    /// </summary>
    [HttpGet("GetCountOfItems")]
    public async Task<int> GetCountOfItems()
    {
        return await _bankEntityService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public async Task<IActionResult> UpdateItemOfBank(int BankId, UpdateItemOfBank updateItemOfBankModel)
    {
        await _bankEntityService.Update(BankId, updateItemOfBankModel);
        return Ok();
    }

    /// <summary>
    /// Получить клетки по дате разморозки
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц</param>
    /// <param name="day">День</param>
    [HttpGet("GetItemsOnDateOfDefrosting")]
    public async Task<IActionResult> GetItemsOnDate(int year, int month, int day)
    {
        var result = await _bankEntityService.GetAllOnDateOfDefrosting(year, month, day);
        return Ok(result);
    }

    /// <summary>
    /// Получить клетки в диапазоне дат разморозки
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="monthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="monthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    [HttpGet("GetItemsOnDateRangeOfDefrosting")]
    public async Task<IActionResult> GetItemsOnDateRangeOfDefrosting(
        int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        var result = await _bankEntityService.GetAllOnDateRangeOfDefrosting(
            yearStart, monthStart, dayStart, yearEnd, monthEnd, dayEnd);
        return Ok(result);
    }

    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    [HttpGet("ExportToCsv")]
    public async Task<IActionResult> ExportToCsv()
    {
        var csvStream = await _bankCsvService.ExportToCsvAsync();
        if (csvStream == null)
        {
            return NotFound("Нет данных для экспорта.");
        }
        return File(csvStream, "text/csv", "BankSecondData.csv");
        
    }

    /// <summary>
    /// Импорт файла CSV
    /// </summary>
    /// <param name="file"></param>
    [HttpPost("ImportFromCsv")]
    public async Task<IActionResult> ImportToCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не был загружен или пуст");
        }

        using (var stream = file.OpenReadStream())
        {
            await _bankCsvService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
}
