using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Game;
using game_store_platform.Core.DTOs.Hotel;

namespace game_store_platform.Core.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllAsync();
    Task<GameDto?> GetByIdAsync(int id);
    Task<GameDto> CreateAsync(CreateGameDto dto);
    Task<bool> UpdateAsync(int id, UpdateGameDto dto);
    Task<bool> DeleteAsync(int id);
}