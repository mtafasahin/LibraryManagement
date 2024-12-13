using LibraryManagementApi.Dtos;
using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(int id);
        Task MoveBooksToShelfAsync(BookShelfDto bookShelfDto);
    }
}