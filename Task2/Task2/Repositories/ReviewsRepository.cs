using Microsoft.EntityFrameworkCore;
using Task2.DataBase;
using Task2.Models;

namespace Task2.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly AppDataContext _context;
        private readonly DbSet<Review> _reviews;

        public ReviewsRepository(AppDataContext context)
        {
            _context = context;
            _reviews = context.Set<Review>();
        }

        public async Task<Guid?> PostAsync(Review review)
        {
            await _reviews.AddAsync(review);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return review.Id;
        }
    }
}
