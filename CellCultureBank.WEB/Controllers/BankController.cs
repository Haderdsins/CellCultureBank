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
    /// Страница редактирования клетки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var bankCell = _bankSecondService.Get(id);
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
            _bankSecondService.UpdateBankCell(id, model); // Обновляем данные клетки
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
        var csvStream = await _bankSecondService.ExportToCsvAsync();
        if (csvStream == null)
        {
            return NotFound("Нет данных для экспорта.");
        }
        return File(csvStream, "text/csv", "BankSecondData.csv");
    }


    public IActionResult Delete(int id)
    {
        _bankSecondService.Delete(id);
        return RedirectToAction("Index");
    }
}