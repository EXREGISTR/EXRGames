namespace EXRGames.Domain {
    public class UserProfile {
        public const int MaxNicknameLength = 50;

        public string Id { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public DateOnly? RegistrationDate { get; set; }

        public ICollection<Friend> Friends { get; set; } = [];
        public ICollection<Game> Games { get; set; } = [];
    }
}
