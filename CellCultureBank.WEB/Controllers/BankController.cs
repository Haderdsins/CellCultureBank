using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.BLL.Services.BankSecondCSV;
using CellCultureBank.BLL.Services.BankSecondEntity;
using Microsoft.AspNetCore.Mvc;

namespace CellCultureBank.WEB.Controllers;

public class BankController : Controller
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
    public async Task<IActionResult> Index()
    {
        var allItems = await _bankSecondEntityService.GetAll();
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
    public async Task<IActionResult> Create(CreateItemOfSecondBank createItemOfSecondBank)
    {
        if (!ModelState.IsValid)
        {
            return View(createItemOfSecondBank);
        }

        await _bankSecondEntityService.Create(createItemOfSecondBank);
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Страница редактирования клетки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var bankCell = await _bankSecondEntityService.Get(id);
        if (bankCell == null)
        {
            return NotFound();
        }

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

        return View(model);
    }

    /// <summary>
    /// Метод редактирования клетки
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateCellModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _bankSecondEntityService.UpdateBankCell(id, model);

        return RedirectToAction("Index");
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
