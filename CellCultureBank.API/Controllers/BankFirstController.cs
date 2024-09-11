using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Delete;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.BLL.Services.BankFirst;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.API.Controllers;
[ApiController]
[Route("[controller]")]
public class BankFirstController : ControllerBase
{
    private readonly IBankFirstService _bankFirstService;
    
    public BankFirstController(IBankFirstService bankFirstService)
    {
        _bankFirstService = bankFirstService;
    }
    /// <summary>
    /// Создать клетку
    /// </summary>
    /// <param name="createItemOfBankModel"></param>
    [HttpPost("CreateItemOfBank")]
    public void CreateItemOfBank(CreateItemOfBankModel createItemOfBankModel)
    {
        _bankFirstService.Create(createItemOfBankModel);
    }
    /// <summary>
    /// Удалить клетку
    /// </summary>
    /// <param name="deleteItemOfBankModelModel"></param>
    [HttpDelete("DeleteItemOfBank")]
    public void DeleteItemOfBank(DeleteItemOfBankModel deleteItemOfBankModelModel)
    {
        _bankFirstService.Delete(deleteItemOfBankModelModel.Id);
    }
    
    /// <summary>
    /// Удалить все клетки
    /// </summary>
    [HttpDelete("DeleteAllItemOfBank")]
    public void DeleteAllItemOfBank()
    {
        _bankFirstService.DeleteAll();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public IActionResult GetItemOfBank(int BankId)//то что написано вот тут и запрашивается у клиента
    {
        var itemOfBank = _bankFirstService.Get(BankId);
        return Ok(itemOfBank) ;
    }
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    [HttpGet("GetAllItemOfBank")]
    public IActionResult GetAllItemOfBank()
    {
        var allItems = _bankFirstService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public IActionResult GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = _bankFirstService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    ///  Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedItemsOfBank")]
    public IActionResult GetSortedItemsOfBank()
    {
        var allSortItems = _bankFirstService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }
    
    /// <summary>
    /// Получить количество записей
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCountOfItems")]
    public int GetCountOfItems()
    {
        return _bankFirstService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(int BankId, UpdateItemOfBankModel updateItemOfBankModel)
    {
        _bankFirstService.Update(BankId,updateItemOfBankModel);
    }

    /// <summary>
    /// Получить клетки по дате
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="mounth">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDate")]
    public IActionResult GetItemsOnDate(int year, int mounth, int day)
    {
        var result =  _bankFirstService.GetAllOnDate(year, mounth, day);
        return Ok(result);
    }

    /// <summary>
    /// Получить клетки в диапозоне дат
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="mounthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="mounthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDateRange")]
    public IActionResult GetItemsOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd)
    {
        var result = _bankFirstService.GetAllOnDateRange(yearStart, mounthStart, dayStart, yearEnd, mounthEnd, dayEnd);
        return Ok(result);
    }

    /// <summary>
    /// Экспорт данных в CSV
    /// </summary>
    /// <returns></returns>
    [HttpGet("ExportToCsv")]
    public async Task<IActionResult> ExportToCsv()
    {
        var csvStream = await _bankFirstService.ExportToCsvAsync();
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
            await _bankFirstService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
}