using LibraryManagementApi.Dtos;
using LibraryManagementApi.Models;
using LibraryManagementApi.Repositories;
using Mapster;

namespace LibraryManagementApi.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<Genre> genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books =  await _bookRepository.GetAllAsync();
            return books.Adapt<IEnumerable<BookDto>>();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book.Adapt<BookDto>();
        }

        public async Task<BookDto> CreateBookAsync(BookDto book)
        {
            // Author kontrolü ve oluşturma
            foreach (var author in book.Authors)
            {
                var existingAuthor = await _authorRepository.FindAsync(a => a.Name == author.Name);
                if (!existingAuthor.Any())
                {
                    await _authorRepository.AddAsync(author.Adapt<Author>());
                }
            }
            var updatedBook = book.Adapt<Book>();
            await _bookRepository.AddAsync(updatedBook);            
            return updatedBook.Adapt<BookDto>();
        }

        public async Task UpdateBookAsync(BookDto book)
        {
            _bookRepository.Update(book.Adapt<Book>());
            await Task.CompletedTask;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                _bookRepository.Remove(book);
            }
        }

    }
}