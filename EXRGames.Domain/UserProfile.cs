namespace EXRGames.Domain {
    public class UserProfile {
        public const int MaxNicknameLength = 50;

        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public DateOnly? RegistrationDate { get; set; }
    }
}
