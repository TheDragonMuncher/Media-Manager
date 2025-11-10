using Media_Manager.Core.DTOs;
using MediaManager.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Media_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _repository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound(new { message = $"Book with id: {id} not found" });
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] CreateBookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                AuthorName = bookDto.AuthorName,
                Title = bookDto.Title,
                Summary = bookDto.Summary,
                Genre = bookDto.Genre,
                ISBN = bookDto.ISBN,
                NumberOfPages = bookDto.NumberOfPages,
                PublicationYear = bookDto.PublicationYear,
                CoverImageURL = bookDto.CoverImageURL,
                CreatedAt = DateTime.UtcNow
            };

            var CreatedBook = await _repository.CreateAsync(book);

            return CreatedAtAction(
                nameof(GetById),
                new { id = CreatedBook.Id },
                CreatedBook
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                AuthorName = bookDto.AuthorName,
                Title = bookDto.Title,
                Summary = bookDto.Summary,
                Genre = bookDto.Genre,
                NumberOfPages = bookDto.NumberOfPages,
                PublicationYear = bookDto.PublicationYear,
                CoverImageURL = bookDto.CoverImageURL,
                CreatedAt = DateTime.UtcNow
            };

            var updatedBook = await _repository.UpdateAsync(book);
            if (updatedBook == null)
            {
                return NotFound(new { message = $"Book with id: {id} not found" });
            }

            return Ok(updatedBook);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook(int id, [FromBody] JsonPatchDocument<Book> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { message = "Patch document is null" });
            }

            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with id: {id} not found" });
            }

            patchDoc.ApplyTo(book);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            book.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(book);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
        {
            return NotFound(new { message = $"Book with id: {id} not found" });
        }
        return NoContent();
        }
    }
}