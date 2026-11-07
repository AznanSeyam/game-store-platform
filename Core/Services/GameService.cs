using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using game_store_platform.Core.DTOs.Game;
using game_store_platform.Core.DTOs.Hotel;
using game_store_platform.Core.Interfaces;
using game_store_platform.Data;
using game_store_platform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_store_platform.Core.Services
{
    public class GameService(GameStoreDBContext context, IMapper mapper) : IGameService
    {
        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await context.Games
                .Include(g => g.Genre)
                .ToListAsync();

            return mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task<GameDto?> GetByIdAsync(int id)
        {
            var game = await context.Games
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return null;

            return mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var game = mapper.Map<Game>(dto);

            context.Games.Add(game);
            await context.SaveChangesAsync();

            var createdGame = await context.Games
                .Include(g => g.Genre)
                .FirstAsync(g => g.Id == game.Id);

            return mapper.Map<GameDto>(createdGame);
        }

        public async Task<bool> UpdateAsync(int id, UpdateGameDto dto)
        {
            var game = await context.Games.FindAsync(id);
            if (game == null) return false;

            mapper.Map(dto, game);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await context.Games.FindAsync(id);
            if (game == null) return false;

            context.Games.Remove(game);
            await context.SaveChangesAsync();
            return true;
        }
    }
}