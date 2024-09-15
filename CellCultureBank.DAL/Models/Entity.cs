namespace CellCultureBank.DAL.Models;

public abstract class Entity<TPrimaryKey>
{
    /// <summary>
    /// Первичный ключ сущности
    /// </summary>
    public TPrimaryKey Id { get; set; }
}

