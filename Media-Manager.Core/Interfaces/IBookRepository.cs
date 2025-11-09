using System;

namespace MediaManager.Core.Models;

public interface IBookRepository
{
    Task<ICollection<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<Book> CreateAsync(Book book);
    Task<bool> DeleteAsync(int id);
    Task<Book> UpdateAsync(Book book);
}
