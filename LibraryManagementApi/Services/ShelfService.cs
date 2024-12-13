using System;
using LibraryManagementApi.Dtos;
using LibraryManagementApi.Models;
using LibraryManagementApi.Repositories;
using Mapster;

namespace LibraryManagementApi.Services
{
    public class ShelfService : IShelfService
    {
        private readonly IRepository<Shelf> _shelfRepository;

        public ShelfService(IRepository<Shelf> shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }

        public async Task<IEnumerable<ShelfDto>> GetAllShelvesAsync()
        {
            var shelves = await _shelfRepository.GetAllAsync();
            return shelves.Adapt<IEnumerable<ShelfDto>>();
        }

        public async Task<ShelfDto> GetShelfByIdAsync(int id)
        {
            var shelf = await _shelfRepository.GetByIdAsync(id);
            return shelf.Adapt<ShelfDto>();
        }

        public async Task<ShelfDto> CreateShelfAsync(ShelfDto shelfDto)
        {
            var shelf = shelfDto.Adapt<Shelf>();
            await _shelfRepository.AddAsync(shelf);
            return shelf.Adapt<ShelfDto>();
        }

        public async Task UpdateShelfAsync(ShelfDto shelfDto)
        {
            var shelf = shelfDto.Adapt<Shelf>();
            await _shelfRepository.UpdateAsync(shelf);
        }

        public async Task DeleteShelfAsync(int id)
        {
            var shelf = await _shelfRepository.GetByIdAsync(id);
            if (shelf != null)
            {
                await _shelfRepository.RemoveAsync(shelf);
            }
        }
    }
}
