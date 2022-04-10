using Task2.Models;

namespace Task2.Repositories
{
    public interface IGamesRepository
    {
        Task<bool> DeleteAsync(Guid id);
        Task<List<Game>> GetAllAsync();
        Task<Game?> GetAsync(Guid id);
        Task<string> PostAsync(Game game);
    }
}