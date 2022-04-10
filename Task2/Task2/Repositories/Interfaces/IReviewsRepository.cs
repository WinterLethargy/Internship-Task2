using Task2.Models;

namespace Task2.Repositories
{
    public interface IReviewsRepository
    {
        Task<string> PostAsync(Review game);
    }
}