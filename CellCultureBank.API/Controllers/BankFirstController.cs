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
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(int BankId, UpdateItemOfBankModel updateItemOfBankModel)
    {
        _bankFirstService.Update(BankId,updateItemOfBankModel);
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





}