using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CellCultureBank.BLL.Services.BankSecondEntity;

public interface IBankSecondEntityService
{
    /// <summary>
    /// Создание клетки
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Create(CreateItemOfSecondBank model);

    /// <summary>
    /// Удалить клетку
    /// </summary>
    /// <param name="BankId">Id клетки</param>
    /// <returns></returns>
    Task Delete(int BankId);

    /// <summary>
    /// Получить клетку
    /// </summary>
    /// <param name="BankId">Id клетки</param>
    /// <returns></returns>
    Task<BankSecond> Get(int BankId);

    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankSecond>> GetAll();

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Id клетки</param>
    /// <param name="model">Модель обновления клетки</param>
    /// <returns></returns>
    Task Update(int BankId, UpdateItemOfSecondBank model);

    /// <summary>
    /// Удалить все клетки
    /// </summary>
    /// <returns></returns>
    Task DeleteAll();

    /// <summary>
    /// Получить все клетки в отсортированном порядке по убыванию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankSecond>> GetSortedDescendingItemsOfBank();

    /// <summary>
    /// Получить все клетки в отсортированном порядке по возрастанию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankSecond>> GetSortedItemsOfBank();

    /// <summary>
    /// Получить все клетки по дате разморозки
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    Task<IEnumerable<BankSecond>> GetAllOnDateOfDefrosting(int year, int month, int day);

    /// <summary>
    /// Получить все клетки в диапазоне дат разморозки
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="monthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="monthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    Task<IEnumerable<BankSecond>> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd);

    /// <summary>
    /// Получить количество клеток
    /// </summary>
    /// <returns></returns>
    Task<int> GetCountOfAllItems();

    /// <summary>
    /// Обновить клетку без дат заморозки и разморозки
    /// </summary>
    /// <param name="id">Id клетки</param>
    /// <param name="model">Модель клетки</param>
    /// <returns></returns>
    Task UpdateBankCell(int id, UpdateCellModel model);
}
