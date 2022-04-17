using Task2.Models;

namespace Task2.Repositories
{
    public interface IReviewsRepository
    {
        public Task<Guid?> PostAsync(Review game);
    }
}