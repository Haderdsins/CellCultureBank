﻿using System.Globalization;
using System.Text;
using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Delete;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.BLL.Services.BankFirst;
using CsvHelper;
using CsvHelper.Configuration;
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
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(int BankId, UpdateItemOfBankModel updateItemOfBankModel)
    {
        _bankFirstService.Update(BankId,updateItemOfBankModel);
    }
    /// <summary>
    /// Получить клетки по дате
    /// </summary>
    /// <param name="Date"></param>
    /// <returns></returns>
    [HttpGet("GetItemsOnDate")]
    public IActionResult GetItemsOnDate(DateTime Date )
    {
        var result =  _bankFirstService.GetAllOnDate(Date);
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