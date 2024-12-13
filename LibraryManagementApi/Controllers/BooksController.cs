using Microsoft.AspNetCore.Mvc;
using LibraryManagementApi.Models;
using LibraryManagementApi.Services;
using LibraryManagementApi.Dtos;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDto book)
        {
            var createdBook = await _bookService.CreateBookAsync(book);
            return CreatedAtAction("GetBook", new { id = createdBook.Id }, createdBook);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDto book)
        {
            book.Id = id;

            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPost("MoveToShelf")]
        public async Task<IActionResult> MoveBooksToShelf(BookShelfDto bookShelfDto)
        {
            await _bookService.MoveBooksToShelfAsync(bookShelfDto);
            return NoContent();
        }
    }
}