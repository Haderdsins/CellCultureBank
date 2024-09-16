using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BankSecondController : ControllerBase
{
    private readonly IBankSecondEntityService _bankSecondEntityService;
    private readonly IBankSecondCsvService _bankSecondCsvService;

    public BankSecondController(IBankSecondEntityService bankSecondEntityService, IBankSecondCsvService bankSecondCsvService)
    {
        _bankSecondEntityService = bankSecondEntityService;
        _bankSecondCsvService = bankSecondCsvService;
    }

    /// <summary>
    /// Создать клетку
    /// </summary>
    /// <param name="createItemOfSecondBank"></param>
    [HttpPost("CreateItemOfSecondBank")]
    public async Task<IActionResult> CreateItemOfSecondBank(CreateItemOfSecondBank createItemOfSecondBank)
    {
        await _bankSecondEntityService.Create(createItemOfSecondBank);
        return Ok();
    }
    
    /// <summary>
    /// Удалить клетку по id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("CreateItemOfSecondBank")]
    public async Task<IActionResult> DeleteItemOfSecondBank(int id)
    {
        await _bankSecondEntityService.Delete(id);
        return Ok();
    }
    
    /// <summary>
    /// Удалить все клетки
    /// </summary>
    [HttpDelete("DeleteAllItemOfBank")]
    public async Task<IActionResult> DeleteAllItemOfBank()
    {
        await _bankSecondEntityService.DeleteAll();
        return Ok();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public async Task<IActionResult> GetItemOfBank(int BankId)
    {
        var itemOfBank = await _bankSecondEntityService.Get(BankId);
        return Ok(itemOfBank);
    }

    /// <summary>
    /// Получить все клетки
    /// </summary>
    [HttpGet("GetAllItemOfBank")]
    public async Task<IActionResult> GetAllItemOfBank()
    {
        var allItems = await _bankSecondEntityService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public async Task<IActionResult> GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = await _bankSecondEntityService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    /// Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    [HttpGet("GetSortedItemsOfBank")]
    public async Task<IActionResult> GetSortedItemsOfBank()
    {
        var allSortItems = await _bankSecondEntityService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }

    /// <summary>
    /// Получить количество записей
    /// </summary>
    [HttpGet("GetCountOfItems")]
    public async Task<int> GetCountOfItems()
    {
        return await _bankSecondEntityService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public async Task<IActionResult> UpdateItemOfBank(int BankId, UpdateItemOfSecondBank updateItemOfBankModel)
    {
        await _bankSecondEntityService.Update(BankId, updateItemOfBankModel);
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
        var result = await _bankSecondEntityService.GetAllOnDateOfDefrosting(year, month, day);
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
        var result = await _bankSecondEntityService.GetAllOnDateRangeOfDefrosting(
            yearStart, monthStart, dayStart, yearEnd, monthEnd, dayEnd);
        return Ok(result);
    }

    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    [HttpGet("ExportToCsv")]
    public async Task<IActionResult> ExportToCsv()
    {
        var csvStream = await _bankSecondCsvService.ExportToCsvAsync();
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
            await _bankSecondCsvService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
}
