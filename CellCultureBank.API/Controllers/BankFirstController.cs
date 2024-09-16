using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Delete;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.BLL.Services.BankFirstCSV;
using CellCultureBank.BLL.Services.BankFirstEntity;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BankFirstController : ControllerBase
{
    private readonly IBankFirstEntityService _bankFirstEntityService;
    private readonly IBankFirstCsvService _bankFirstCsvService;
    
    public BankFirstController(IBankFirstEntityService bankFirstEntityService, IBankFirstCsvService bankFirstCsvService)
    {
        _bankFirstEntityService = bankFirstEntityService;
        _bankFirstCsvService = bankFirstCsvService;
    }

    /// <summary>
    /// Создать клетку
    /// </summary>
    /// <param name="createItemOfBankModel"></param>
    [HttpPost("CreateItemOfBank")]
    public async Task<IActionResult> CreateItemOfBank(CreateItemOfBankModel createItemOfBankModel)
    {
        await _bankFirstEntityService.Create(createItemOfBankModel);
        return Ok();
    }

    /// <summary>
    /// Удалить клетку
    /// </summary>
    /// <param name="deleteItemOfBankModelModel"></param>
    [HttpDelete("DeleteItemOfBank")]
    public async Task<IActionResult> DeleteItemOfBank(int deleteItemOfBankModelModelId)
    {
        await _bankFirstEntityService.Delete(deleteItemOfBankModelModelId);
        return Ok();
    }

    /// <summary>
    /// Удалить все клетки
    /// </summary>
    [HttpDelete("DeleteAllItemOfBank")]
    public async Task<IActionResult> DeleteAllItemOfBank()
    {
        await _bankFirstEntityService.DeleteAll();
        return Ok();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public async Task<IActionResult> GetItemOfBank(int BankId)
    {
        var itemOfBank = await _bankFirstEntityService.Get(BankId);
        return Ok(itemOfBank);
    }
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllItemOfBank")]
    public async Task<IActionResult> GetAllItemOfBank()
    {
        var allItems = await _bankFirstEntityService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public async Task<IActionResult> GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = await _bankFirstEntityService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    /// Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedItemsOfBank")]
    public async Task<IActionResult> GetSortedItemsOfBank()
    {
        var allSortItems = await _bankFirstEntityService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }
    
    /// <summary>
    /// Получить количество записей
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCountOfItems")]
    public async Task<int> GetCountOfItems()
    {
        return await _bankFirstEntityService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public async Task<IActionResult> UpdateItemOfBank(int BankId, UpdateItemOfBankModel updateItemOfBankModel)
    {
        await _bankFirstEntityService.Update(BankId, updateItemOfBankModel);
        return Ok();
    }

    /// <summary>
    /// Получить клетки по дате
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="mounth">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDate")]
    public async Task<IActionResult> GetItemsOnDate(int year, int mounth, int day)
    {
        var result = await _bankFirstEntityService.GetAllOnDate(year, mounth, day);
        return Ok(result);
    }

    /// <summary>
    /// Получить клетки в диапазоне дат
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="mounthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="mounthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDateRange")]
    public async Task<IActionResult> GetItemsOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd)
    {
        var result = await _bankFirstEntityService.GetAllOnDateRange(yearStart, mounthStart, dayStart, yearEnd, mounthEnd, dayEnd);
        return Ok(result);
    }

    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    /// <returns></returns>
    [HttpGet("ExportToCsv")]
    public async Task<IActionResult> ExportToCsv()
    {
        var csvStream = await _bankFirstCsvService.ExportToCsvAsync();
        if (csvStream == null)
        {
            return NotFound("Нет данных для экспорта.");
        }
        return File(csvStream, "text/csv", "BankFirstData.csv");
    }

    /// <summary>
    /// Импорт файла CSV
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("ImportFromCsv")]
    public async Task<IActionResult> ImportToCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не был загружен или пуст");
        }

        using (var stream = file.OpenReadStream())
        {
            await _bankFirstCsvService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
}
