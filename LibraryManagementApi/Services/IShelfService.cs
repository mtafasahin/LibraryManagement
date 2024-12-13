using System;
using LibraryManagementApi.Dtos;

namespace LibraryManagementApi.Services
{
    public interface IShelfService
    {
        Task<IEnumerable<ShelfDto>> GetAllShelvesAsync();
        Task<ShelfDto> GetShelfByIdAsync(int id);
        Task<ShelfDto> CreateShelfAsync(ShelfDto shelfDto);
        Task UpdateShelfAsync(ShelfDto shelfDto);
        Task DeleteShelfAsync(int id);
    }
}
