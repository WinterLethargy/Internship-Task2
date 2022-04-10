namespace Task2.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public string? Text { get; set; }
        public int Rating { get; set; }
    }
}
