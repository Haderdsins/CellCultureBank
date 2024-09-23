using CellCultureBank.BLL.Models;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.BankEntity;

public interface IBankEntityService
{
    /// <summary>
    /// Создание клетки
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Create(CreateItemModel model);

    /// <summary>
    /// Удалить клетку
    /// </summary>
    /// <param name="bankId">Id клетки</param>
    /// <returns></returns>
    Task Delete(int bankId);

    /// <summary>
    /// Получить клетку
    /// </summary>
    /// <param name="bankId">Id клетки</param>
    /// <returns></returns>
    Task<BankOfCell> Get(int bankId);

    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankOfCell>> GetAll();

    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="bankId">Id клетки</param>
    /// <param name="model">Модель обновления клетки</param>
    /// <returns></returns>
    Task Update(int bankId, UpdateItemModel model);
    
    /// <summary>
    /// Получить все клетки в отсортированном порядке по убыванию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankOfCell>> GetSortedDescendingItemsOfBank();

    /// <summary>
    /// Получить все клетки в отсортированном порядке по возрастанию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankOfCell>> GetSortedItemsOfBank();

    /// <summary>
    /// Получить все клетки по дате разморозки
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    Task<IEnumerable<BankOfCell>> GetAllOnDateOfDefrosting(int year, int month, int day);

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
    Task<IEnumerable<BankOfCell>> GetAllOnDateRangeOfDefrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd);
    
    /// <summary>
    /// Получить все клетки в диапазоне дат заморозки
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="monthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="monthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    Task<IEnumerable<BankOfCell>> GetAllOnDateRangeOfFrosting(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd);
    
    /// <summary>
    /// Получить количество клеток
    /// </summary>
    /// <returns></returns>
    Task<int> GetCountOfAllItems();

    /// <summary>
    /// Заморозить клетку
    /// </summary>
    /// <returns></returns>
    Task FreezeCell(int bankId, FreezeCellModel model);

    /// <summary>
    /// Разморозить клетку
    /// </summary>
    /// <returns></returns>
    Task DefrostCell(int bankId, DefrostCellModel model);
}
