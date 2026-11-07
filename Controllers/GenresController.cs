using game_store_platform.Core.DTOs.Country;
using game_store_platform.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_store_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await genreService.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await genreService.GetByIdAsync(id);
            if (genre == null) return NotFound($"Genre with Id {id} not found.");
            return Ok(genre);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateGenreDto dto)
        {
            var created = await genreService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDto dto)
        {
            var isUpdated = await genreService.UpdateAsync(id, dto);
            if (!isUpdated) return NotFound($"Genre with Id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await genreService.DeleteAsync(id);
            if (!isDeleted) return NotFound($"Genre with Id {id} not found.");
            return NoContent();
        }
    }
}