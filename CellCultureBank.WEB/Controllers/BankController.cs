using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Services.BankSecond;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CellCultureBank.WEB.Controllers;

public class BankController: Controller
{
    private readonly IBankSecondService _bankSecondService;

    public BankController(IBankSecondService bankSecondService)
    {
        _bankSecondService = bankSecondService;
    }
    /// <summary>
    /// Страница всех клеток
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        var allItems = _bankSecondService.GetAll();
        return View(allItems);
    }
    /// <summary>
    /// Страница создания новой клетки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    
    /// <summary>
    /// Эскпорт данных в CSV
    /// </summary>
    /// <returns></returns>
    [HttpGet("ExportToCsv")]
    public async Task<IActionResult> ExportToCsv()
    {
        var csvStream = await _bankSecondService.ExportToCsvAsync();
        if (csvStream == null)
        {
            return NotFound("Нет данных для экспорта.");
        }
        return File(csvStream, "text/csv", "BankSecondData.csv");
    }

    /// <summary>
    /// Удалить клетку по id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("CreateItemOfSecondBank")]
    public void CreateItemOfSecondBank(int id) 
    {
        _bankSecondService.Delete(id);
    }
    /// <summary>
    /// Создать клетку
    /// </summary>
    /// <param name="createItemOfSecondBank"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateItemOfSecondBank createItemOfSecondBank)
    {
        if (ModelState.IsValid)
        {
            _bankSecondService.Create(createItemOfSecondBank);
            return RedirectToAction("Index");  // Перенаправление на список клеток после добавления
        }

        // Если данные не валидны, возвращаем форму с ошибками
        return View(createItemOfSecondBank);
    }
    
    /// <summary>
    /// Удалить все клетки
    /// </summary>
    [HttpDelete("DeleteAllItemOfBank")]
    public void DeleteAllItemOfBank()
    {
        _bankSecondService.DeleteAll();
    }

    /// <summary>
    /// Получить клетку по id
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    /// <param name="BankId">Идентификатор клетки</param>
    [HttpGet("GetItemOfBank")]
    public IActionResult GetItemOfBank(int BankId)//то что написано вот тут и запрашивается у клиента
    {
        var itemOfBank = _bankSecondService.Get(BankId);
        return Ok(itemOfBank) ;
    }
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    [HttpGet("GetAllItemOfBank")]
    public IActionResult GetAllItemOfBank()
    {
        var allItems = _bankSecondService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Получить все клетки (сортировка по дате на убывание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedDescendingItemsOfBank")]
    public IActionResult GetSortedDescendingItemsOfBank()
    {
        var allSortDesItems = _bankSecondService.GetSortedDescendingItemsOfBank();
        return Ok(allSortDesItems);
    }
    
    /// <summary>
    ///  Получить все клетки (сортировка по дате на возрастание)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetSortedItemsOfBank")]
    public IActionResult GetSortedItemsOfBank()
    {
        var allSortItems = _bankSecondService.GetSortedItemsOfBank();
        return Ok(allSortItems);
    }
    
    /// <summary>
    /// Получить количество записей
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCountOfItems")]
    public int GetCountOfItems()
    {
        return _bankSecondService.GetCountOfAllItems();
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Идентификатор клетки</param>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(int BankId, UpdateItemByIdOfSecondBank updateItemOfBankModel)
    {
        _bankSecondService.Update(BankId,updateItemOfBankModel);
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
        var result =  _bankSecondService.GetAllOnDateOfDefrosting(year, mounth, day);
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
        var result = _bankSecondService.GetAllOnDateRangeOfDefrosting(yearStart, mounthStart, dayStart, yearEnd, mounthEnd, dayEnd);
        return Ok(result);
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
            await _bankSecondService.ImportFromCsvAsync(stream);
        }

        return Ok("Данные успешно импортированы");
    }
    
}