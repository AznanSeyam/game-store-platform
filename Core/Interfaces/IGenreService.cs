using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Core.DTOs.Country;

namespace game_store_platform.Core.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task<GenreDto?> GetByIdAsync(int id);
    Task<GenreDto> CreateAsync(CreateGenreDto dto);
    Task<bool> UpdateAsync(int id, UpdateGenreDto dto);
    Task<bool> DeleteAsync(int id);
}