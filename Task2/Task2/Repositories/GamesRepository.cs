using Microsoft.EntityFrameworkCore;
using Task2.DataBase;
using Task2.Models;

namespace Task2.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly AppDataContext _context;
        private readonly DbSet<Game> _games;

        public GamesRepository(AppDataContext context)
        {
            _context = context;
            _games = context.Set<Game>();
        }

        public async Task<Game?> GetAsync(Guid id)
        {
            return await _games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var game = await _games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return false;
            }

            _games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _games
                .OrderByDescending(g => g.Reviews.Average(r => r.Rating))
                .ToListAsync();
        }

        public async Task<Guid?> PostAsync(Game game)
        {
            await _games.AddAsync(game);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return game.Id;
        }
    }
}
