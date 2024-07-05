namespace EXRGames.Domain {
    public class Friend {
        public Guid FirstId { get; set; }
        public UserProfile FirstProfile { get; set; } = null!;

        public Guid SecondId { get; set; }
        public UserProfile SecondProfile { get; set; } = null!;
    }
}
