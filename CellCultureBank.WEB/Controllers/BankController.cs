using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;
using Microsoft.AspNetCore.Mvc;


namespace CellCultureBank.WEB.Controllers;

public class BankController: Controller
{
    private readonly IBankSecondEntityService _bankSecondEntityService;
    private readonly IBankSecondCsvService _bankSecondCsvService;

    public BankController(IBankSecondEntityService bankSecondEntityService, IBankSecondCsvService bankSecondCsvService)
    {
        _bankSecondEntityService = bankSecondEntityService;
        _bankSecondCsvService = bankSecondCsvService;
    }
    /// <summary>
    /// Страница всех клеток
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        var allItems = _bankSecondEntityService.GetAll();
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
    /// Метод создания клетки
    /// </summary>
    /// <param name="createItemOfSecondBank"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateItemOfSecondBank createItemOfSecondBank)
    {
        if (ModelState.IsValid)
        {
            _bankSecondEntityService.Create(createItemOfSecondBank);
            return RedirectToAction("Index");  // Перенаправление на список клеток после добавления
        }

        // Если данные не валидны, возвращаем форму с ошибками
        return View(createItemOfSecondBank);
    }
    
    /// <summary>
    /// Страница редактирования клетки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var bankCell = _bankSecondEntityService.Get(id);
        if (bankCell == null)
        {
            return NotFound();
        }

        // Преобразуем модель из базы данных в модель для представления
        var model = new UpdateCellModel
        { 
            Id = bankCell.Id,
            CellLine = bankCell.CellLine,
            Clearing = bankCell.Clearing,
            Certification = bankCell.Certification,
            Address = bankCell.Address,
            Quantity = bankCell.Quantity,
            Origin = bankCell.Origin
        };

        return View(model);  // Отправляем в представление
    }
    /// <summary>
    /// Метод редактирования клетки
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Edit(int id, UpdateCellModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model); // Если модель недействительна, возвращаем её обратно
        }

        try
        {
            _bankSecondEntityService.UpdateBankCell(id, model); // Обновляем данные клетки
            return RedirectToAction("Index"); // После успешного обновления перенаправляем на список клеток
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model); // Возвращаем модель с ошибкой обратно на страницу
        }
    }

    /// <summary>
    /// Эскпорт данных в CSV
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


    public IActionResult Delete(int id)
    {
        _bankSecondEntityService.Delete(id);
        return RedirectToAction("Index");
    }

    public IActionResult GetOnDateOfDefrosting()
    {
        throw new NotImplementedException();
    }

    public IActionResult GetAllOnDateRangeOfDefrosting()
    {
        throw new NotImplementedException();
    }
}