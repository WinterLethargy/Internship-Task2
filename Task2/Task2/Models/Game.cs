namespace Task2.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
