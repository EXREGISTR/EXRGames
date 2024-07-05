namespace EXRGames.Domain {
    public class Game {
        public const int MaxTitleLength = 100;

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];
    }
}
