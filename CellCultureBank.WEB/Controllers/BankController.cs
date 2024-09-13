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
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var item = _bankSecondService.GetAll().Where(p=>p.Id==id); // Получение элемента по id
        if (item == null)
        {
            return NotFound();
        }

        _bankSecondService.Delete(id); // Удаление клетки
        return RedirectToAction("Index"); // Возвращение на главную страницу после удаления
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


    
}