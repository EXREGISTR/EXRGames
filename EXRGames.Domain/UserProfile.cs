namespace EXRGames.Domain {
    public class UserProfile {
        public string Id { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public DateOnly RegistrationDate { get; set; }

        public ICollection<UserRelationship> Friends { get; set; } = [];
        public ICollection<Game> Games { get; set; } = [];
    }
}
