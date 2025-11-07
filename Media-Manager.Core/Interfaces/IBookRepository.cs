using System;
using Media_Manager.Core.Models;

namespace Media_Manager.Core.Interfaces;

public interface IBookRepository
{
    Task<ICollection<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<Book> CreateAsync(Book book);
    Task<bool> DeleteAsync(int id);
    Task<Book> UpdateAsync(Book book);
}
