using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public string? Text { get; set; }

        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }
    }
}
