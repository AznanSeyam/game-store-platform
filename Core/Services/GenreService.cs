using AutoMapper;
using game_store_platform.Core.Interfaces;
using game_store_platform.Data;
using game_store_platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using game_store_platform.Core.DTOs.Genre;

namespace game_store_platform.Core.Services;

public class GenreService(GameStoreDBContext context, IMapper mapper) : IGenreService
{
    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var genres = await context.Genres.ToListAsync();
        return mapper.Map<IEnumerable<GenreDto>>(genres);
    }

    public async Task<GenreDto?> GetByIdAsync(int id)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre == null) return null;

        return mapper.Map<GenreDto>(genre);
    }

    public async Task<GenreDto> CreateAsync(CreateGenreDto dto)
    {
        var genre = mapper.Map<Genre>(dto);

        context.Genres.Add(genre);
        await context.SaveChangesAsync();

        return mapper.Map<GenreDto>(genre);
    }

    public async Task<bool> UpdateAsync(int id, UpdateGenreDto dto)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre == null) return false;

        mapper.Map(dto, genre);

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre == null) return false;

        context.Genres.Remove(genre);
        await context.SaveChangesAsync();
        return true;
    }
}