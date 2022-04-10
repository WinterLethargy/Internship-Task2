using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace Task2.DataBase
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var game1 = new Game { Id = Guid.NewGuid(), Name = "DarkSouls", Genre = "превозмогание" };
            var game2 = new Game { Id = Guid.NewGuid(), Name = "Genshin Impact", Genre = "аниме девочки" };

            var review1 = new Review { Id = Guid.NewGuid(), GameId = game1.Id, Rating = 0, Text = "я просто птичка, мне такое сложнаа.." };
            var review2 = new Review { Id = Guid.NewGuid(), GameId = game1.Id, Rating = 10, Text = "глубокий лор, крутая боевая система, игра на века" };
            var review3 = new Review { Id = Guid.NewGuid(), GameId = game1.Id, Rating = 5, Text = "игра классная, никому не советую" };

            var review4 = new Review { Id = Guid.NewGuid(), GameId = game2.Id, Rating = 10, Text = "милая графики, милые аниме девочки." };
            var review5 = new Review { Id = Guid.NewGuid(), GameId = game2.Id, Rating = 8, Text = "не играл, но арты по игре крутые" };

            modelBuilder.Entity<Game>().HasData(game1, game2);
            modelBuilder.Entity<Review>().HasData(review1, review2, review3, review4, review5);

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Game)
            //    .WithMany(r => r.Reviews)
            //    .HasForeignKey(r => r.GameId); // совпадает с условностями, потому излишне

            modelBuilder.Entity<Review>().HasCheckConstraint("Rating", "Rating >= 0 AND Rating <= 10");

        }
    }
}
