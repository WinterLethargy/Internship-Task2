using Task2.Models;

namespace Task2.Repositories
{
    public interface IGamesRepository
    {
        public Task<bool> DeleteAsync(Guid id);
        public Task<List<Game>> GetAllAsync();
        public Task<Game?> GetAsync(Guid id);
        public Task<Guid?> PostAsync(Game game);
    }
}