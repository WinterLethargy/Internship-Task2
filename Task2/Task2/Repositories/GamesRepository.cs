using Microsoft.EntityFrameworkCore;
using Task2.DataBase;
using Task2.Models;

namespace Task2.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        readonly AppDataContext _context;
        readonly DbSet<Game> _games;
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

            if (game != null)
            {
                _games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _games
                //.Include(g => g.Reviews)
                .OrderByDescending(g => g.Reviews.Average(r => r.Rating))
                .ToListAsync();
        }

        public async Task<string> PostAsync(Game game)
        {
            await _games.AddAsync(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.Message;
            }
            return string.Empty;
        }
    }
}
