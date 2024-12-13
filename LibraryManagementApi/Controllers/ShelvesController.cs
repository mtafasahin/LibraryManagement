using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementApi.Dtos;
using LibraryManagementApi.Services;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelvesController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        // GET: api/Shelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetShelves()
        {
            var shelves = await _shelfService.GetAllShelvesAsync();
            return Ok(shelves);
        }

        // GET: api/Shelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelfDto>> GetShelf(int id)
        {
            var shelf = await _shelfService.GetShelfByIdAsync(id);

            if (shelf == null)
            {
                return NotFound();
            }

            return Ok(shelf);
        }

        // POST: api/Shelves
        [HttpPost]
        public async Task<ActionResult<ShelfDto>> PostShelf(ShelfDto shelfDto)
        {
            var createdShelf = await _shelfService.CreateShelfAsync(shelfDto);
            return CreatedAtAction("GetShelf", new { id = createdShelf.Id }, createdShelf);
        }

        // PUT: api/Shelves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelf(int id, ShelfDto shelfDto)
        {
            if (id != shelfDto.Id)
            {
                return BadRequest();
            }

            await _shelfService.UpdateShelfAsync(shelfDto);
            return NoContent();
        }

        // DELETE: api/Shelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            await _shelfService.DeleteShelfAsync(id);
            return NoContent();
        }
    }
}
