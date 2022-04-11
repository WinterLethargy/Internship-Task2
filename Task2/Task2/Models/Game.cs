using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
