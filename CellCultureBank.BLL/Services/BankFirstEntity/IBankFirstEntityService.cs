using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Services.BankFirstEntity;

public interface IBankFirstEntityService
{
    /// <summary>
    /// Создание клетки
    /// </summary>
    /// <param name="model"></param>
    Task Create(CreateItemOfBankModel model);
    
    /// <summary>
    /// Удалить клетку
    /// </summary>
    /// <param name="BankId"></param>
    Task Delete(int BankId);
    
    /// <summary>
    /// Получить клетку
    /// </summary>
    /// <param name="BankId">Id клетки</param>
    /// <returns></returns>
    Task <BankFirst> Get(int BankId);
    
    /// <summary>
    /// Получить все клетки
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankFirst>> GetAll(); 
    
    /// <summary>
    /// Обновить данные о клетке
    /// </summary>
    /// <param name="BankId">Id клетки</param>
    /// <param name="model">Модель для обновления</param>
    Task Update(int BankId, UpdateItemOfBankModel model);

    /// <summary>
    /// Удалить все клетки
    /// </summary>
    Task DeleteAll();
    
    /// <summary>
    /// Получить все клетки в отсортированном порядке по убыванию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankFirst>> GetSortedDescendingItemsOfBank();
    
    /// <summary>
    /// Получить все клетки в отсортированном порядке по возрастанию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BankFirst>> GetSortedItemsOfBank();
    
    /// <summary>
    /// Получить все клетки по дате
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="mounth">Месяц</param>
    /// <param name="day">День</param>
    /// <returns></returns>
    Task<IEnumerable<BankFirst>> GetAllOnDate(int year, int mounth, int day);
    
    /// <summary>
    /// Получить все клетки в диапозоне дат
    /// </summary>
    /// <param name="yearStart">Начальный год</param>
    /// <param name="mounthStart">Начальный месяц</param>
    /// <param name="dayStart">Начальный день</param>
    /// <param name="yearEnd">Конечный год</param>
    /// <param name="mounthEnd">Конечный месяц</param>
    /// <param name="dayEnd">Конечный день</param>
    /// <returns></returns>
    Task<IEnumerable<BankFirst>> GetAllOnDateRange(int yearStart, int mounthStart, int dayStart, int yearEnd, int mounthEnd, int dayEnd);
    
    /// <summary>
    /// Получить количество клеток
    /// </summary>
    /// <returns></returns>
    Task<int> GetCountOfAllItems();
}