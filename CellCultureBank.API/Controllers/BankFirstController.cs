using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Delete;
using CellCultureBank.BLL.Models.Get;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.BLL.Services.BankFirst;
using CellCultureBank.DAL.Models;
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
    /// Получить клетку по id (не работает)
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    [HttpGet("GetItemOfBank")]
    public void GetItemOfBank(GetItemsOfBankModel getItemsOfBankModel)
    {
        _bankFirstService.Get(getItemsOfBankModel.Id);
    }
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <param name="getItemsOfBankModel"></param>
    [HttpGet("GetAllItemOfBank")]
    public OkObjectResult GetAllItemOfBank()
    {
        var allItems = _bankFirstService.GetAll();
        return Ok(allItems);
    }

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="updateItemOfBankModel"></param>
    [HttpPut("UpdateItemOfBank")]
    public void UpdateItemOfBank(UpdateItemOfBankModel updateItemOfBankModel)
    {
        _bankFirstService.Update(updateItemOfBankModel.BankId,updateItemOfBankModel);
    }
    
    
}