using LibraryManagementApi.Dtos;
using LibraryManagementApi.Models;
using LibraryManagementApi.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Shelf> _shelfRepository;

        public BookService(IRepository<Book> bookRepository, 
            IRepository<Author> authorRepository, 
            IRepository<Genre> genreRepository,
            IRepository<Shelf> shelfRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _shelfRepository = shelfRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync(b=> b.Authors);
            return books.Adapt<IEnumerable<BookDto>>();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id, b => b.Authors);
            return book.Adapt<BookDto>();
        }

        private async Task<List<Author>> AuthorCheck(IEnumerable<AuthorDto> authorDtos)
        {
            var authors = new List<Author>();
            foreach (var authorDto in authorDtos)
            {
                var existingAuthor = await _authorRepository.FindAsync(a => a.Name == authorDto.Name);
                if (existingAuthor.Any())
                {
                    authors.Add(existingAuthor.First());
                }else{
                    var newAuthor = authorDto.Adapt<Author>();
                    await _authorRepository.AddAsync(newAuthor);
                    authors.Add(newAuthor);
                }
            }
            return authors;
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            
            var book = new Book() {
                Title = bookDto.Title,
                SubTitle = bookDto.SubTitle,
                Description = bookDto.Description,
                IsPhysical = bookDto.IsPhysical,
                ExistInLibrary = bookDto.ExistInLibrary,
                ISBN = bookDto.ISBN,
                PageCount = bookDto.PageCount,
                FileFormat = bookDto.FileFormat,
                FileSize = bookDto.FileSize??0,
                Edition = bookDto.Edition,
                PublicationYear = bookDto.PublicationYear,
                PublisherId = bookDto.PublisherId,
                ShelfId = bookDto.ShelfId
            };
            // Author kontrolü ve oluşturma
            book.Authors = await AuthorCheck(bookDto.Authors);
            
            // Aynı yazar, aynı isim ve aynı edition'lı kitabı kontrol et
            if (await BookExistsAsync(book.Adapt<BookDto>()))
            {
                throw new InvalidOperationException("A book with the same title, edition, and author already exists.");
            }

            

            
            await _bookRepository.AddAsync(book);
            return book.Adapt<BookDto>();
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            // Author kontrolü ve oluşturma
            var authors = await AuthorCheck(bookDto.Authors);

            var book = await _bookRepository.GetByIdAsync(bookDto.Id.Value, b => b.Authors);
            if (book == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            // Mevcut yazarları güncelle
            book.Authors = authors;
            // Diğer alanları güncelle
            book.Title = bookDto.Title;
            book.SubTitle  = bookDto.SubTitle;
            book.Description = bookDto.Description;
            book.IsPhysical = bookDto.IsPhysical;
            book.ISBN = bookDto.ISBN;
            book.PageCount = bookDto.PageCount;
            book.FileFormat = bookDto.FileFormat;
            book.FileSize = bookDto.FileSize??0;
            book.Edition = bookDto.Edition;
            book.PublicationYear = bookDto.PublicationYear;
            book.ExistInLibrary = bookDto.ExistInLibrary;
            book.Edition = bookDto.Edition;
            // book.Publisher 
            await _bookRepository.UpdateAsync(book);           
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await _bookRepository.RemoveAsync(book);
            }
        }

        public async Task<bool> BookExistsAsync(BookDto bookDto)
        {
            var existingBooks = await _bookRepository.FindAsync(b =>
                b.Title == bookDto.Title &&
                (string.IsNullOrEmpty(bookDto.Edition) || b.Edition == bookDto.Edition) &&
                (!bookDto.Authors.Any() || b.Authors.Any(a => bookDto.Authors.Select(ba => ba.Id).Contains(a.Id)))
            );

            return existingBooks.Any();
        }

        public async Task MoveBooksToShelfAsync(BookShelfDto bookShelfDto)
        {
            var shelf = await _shelfRepository.GetByIdAsync(bookShelfDto.ShelfId);
            if (shelf == null)
            {
                throw new InvalidOperationException("Shelf not found.");
            }

            foreach (var bookId in bookShelfDto.BookIds)
            {
                var book = await _bookRepository.GetByIdAsync(bookId);
                if (book != null)
                {
                    book.ShelfId = bookShelfDto.ShelfId;
                    await _bookRepository.UpdateAsync(book);
                }
            }
        }

        
    }
}