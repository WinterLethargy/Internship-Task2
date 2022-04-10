using Microsoft.EntityFrameworkCore;
using Task2.DataBase;
using Task2.Models;

namespace Task2.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        readonly AppDataContext _context;
        readonly DbSet<Review> _reviews;
        public ReviewsRepository(AppDataContext context)
        {
            _context = context;
            _reviews = context.Set<Review>();
        }
        public async Task<string> PostAsync(Review review)
        {
            await _reviews.AddAsync(review);
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
