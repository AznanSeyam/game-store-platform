using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Game;
using game_store_platform.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_store_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController(IGameService gameService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await gameService.GetAllAsync();
            return Ok(games);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await gameService.GetByIdAsync(id);
            if (game == null) return NotFound($"Game with Id {id} not found.");
            return Ok(game);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
        {
            var created = await gameService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameDto dto)
        {
            var isUpdated = await gameService.UpdateAsync(id, dto);
            if (!isUpdated) return NotFound($"Game with Id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await gameService.DeleteAsync(id);
            if (!isDeleted) return NotFound($"Game with Id {id} not found.");
            return NoContent();
        }
    }
}