using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace MediaManager.Infrastructure.Repositories;


public class BookRepository : IBookRepository
{

    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateAsync(Book book)
    {
        book.CreatedAt = DateTime.UtcNow;

        var MediaObject = new MediaObject
        {
            Id = book.Id,
            Type = Core.Enums.MediaObjectTypeEnum.Book,
            Book = book
        };

        _context.Books.Add(book);
        _context.MediaObjects.Add(MediaObject);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var currentBook = await _context.Books.FindAsync(id);
        if (currentBook == null)
        {
            return false;
        }

        _context.Books.Remove(currentBook);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> UpdateAsync(Book book)
    {   
        var currentBook = await _context.Books.FindAsync(book.Id);
        if (currentBook == null)
        {
            return null;
        }

        currentBook.AuthorName = book.AuthorName;
        currentBook.Title = book.Title;
        currentBook.Summary = book.Summary;
        currentBook.Genre = book.Genre;
        currentBook.NumberOfPages = book.NumberOfPages;
        currentBook.PublicationYear = book.PublicationYear;
        currentBook.CoverImageURL = book.CoverImageURL;
        currentBook.UpdatedAt = DateTime.UtcNow;

        _context.Books.Update(currentBook);
        await _context.SaveChangesAsync();
        return currentBook;
    }
}