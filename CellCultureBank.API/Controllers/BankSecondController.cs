using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;
[ApiController]
[Route("[controller]")]
public class BankSecondController: ControllerBase
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
    public void CreateItemOfSecondBank(CreateItemOfSecondBank createItemOfSecondBank) 
    {
        _bankSecondEntityService.Create(createItemOfSecondBank);
    }
    
    /// <summary>
    /// Удалить клетку по id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("CreateItemOfSecondBank")]
    public void CreateItemOfSecondBank(int id) 
    {
        _bankSecondEntityService.Delete(id);
    }
    
    
    /// <summary>
    /// Удалить все клетки
    /// </summary>
    [HttpDelete("DeleteAllItemOfBank")]
    public void DeleteAllItemOfBank()
    {
        _bankSecondEntityService.DeleteAll();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public IActionResult GetItemOfBank(int BankId)//то что написано вот тут и запрашивается у клиента
    {
        var itemOfBank = _bankSecondEntityService.Get(BankId);
        return Ok(itemOfBank) ;
    }
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    [HttpGet("GetAllItemOfBank")]
    public IActionResult GetAllItemOfBank()
    {
        var allItems = _bankSecondEntityService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public IActionResult GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = _bankSecondEntityService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    ///  Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedItemsOfBank")]
    public IActionResult GetSortedItemsOfBank()
    {
        var allSortItems = _bankSecondEntityService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }
    
    /// <summary>
    /// Получить количество записей
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCountOfItems")]
    public int GetCountOfItems()
    {
        return _bankSecondEntityService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(int BankId, UpdateItemOfSecondBank updateItemOfBankModel)
    {
        _bankSecondEntityService.Update(BankId,updateItemOfBankModel);
    }

    /// <summary>
    /// Получить клетки по дате разморозки
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="mounth">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDateOfDefrosting")]
    public IActionResult GetItemsOnDate(int year, int mounth, int day)
    {
        var result =  _bankSecondEntityService.GetAllOnDateOfDefrosting(year, mounth, day);
        return Ok(result);
    }

    /// <summary>
    /// Получить клетки в диапозоне дат разморозок
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="mounthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="mounthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDateRangeOfDefrosting")]
    public IActionResult GetItemsOnDateRangeOfDefrosting(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd)
    {
        var result = _bankSecondEntityService.GetAllOnDateRangeOfDefrosting(yearStart, mounthStart, dayStart, yearEnd, mounthEnd, dayEnd);
        return Ok(result);
    }

    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    /// <returns></returns>
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
            await _bankSecondCsvService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
}