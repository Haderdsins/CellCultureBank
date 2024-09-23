using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.BLL.Services.BankSecondEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using CellCultureBank.BLL.Profile.Services.UserService;
using CellCultureBank.BLL.Services.BankSecondCSV;

namespace CellCultureBank.WEB.Controllers;

/// <summary>
/// Контроллер взаимодействия с банком клеточных культур
/// </summary>
public class BankController : Controller
{
    private readonly IBankSecondEntityService _bankSecondEntityService;
    private readonly IBankSecondCsvService _bankSecondCsvService;
    private readonly IUserService _userService;

    public BankController(IBankSecondEntityService bankSecondEntityService, IBankSecondCsvService bankSecondCsvService, IUserService userService)
    {
        _bankSecondEntityService = bankSecondEntityService;
        _bankSecondCsvService = bankSecondCsvService;
        _userService = userService;
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
    public async Task<IActionResult> Create()
    {
        var users = await _userService.GetAllUsers(); // Получаем всех пользователей
        ViewBag.Users = new SelectList(users, "Id", "FullName"); // Передаем в представление

        var model = new CreateItemOfBank(); // Создаем пустую модель для создания новой клетки
        return View(model);
    }

    /// <summary>
    /// Метод создания клетки
    /// </summary>
    /// <param name="createItemOfBank"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateItemOfBank createItemOfBank)
    {
        if (!ModelState.IsValid)
        {
            return View(createItemOfBank);
        }

        await _bankSecondEntityService.Create(createItemOfBank);
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

    /// <summary>
    /// Импорт файла CSV
    /// </summary>
    /// <param name="file"></param>
    [HttpPost("Bank/ImportFromCsv")]
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

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Метод удаления клетки
    /// </summary>
    /// <param name="id"></param>
    public async Task<IActionResult> Delete(int id)
    {
        await _bankSecondEntityService.Delete(id);
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Получение клеток по дате разморозки
    /// </summary>
    public async Task<IActionResult> GetOnDateOfDefrosting(int year, int month, int day)
    {
        var cells = await _bankSecondEntityService.GetAllOnDateOfDefrosting(year, month, day);
        return View("Index", cells);
    }

    /// <summary>
    /// Получение клеток в диапазоне дат разморозки
    /// </summary>
    public async Task<IActionResult> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
    {
        var cells = await _bankSecondEntityService.GetAllOnDateRangeOfDefrosting(yearStart, monthStart, dayStart, yearEnd, monthEnd, dayEnd);
        return View("Index", cells);
    }
}
