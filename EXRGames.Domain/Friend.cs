namespace EXRGames.Domain {
    public class Friend {
        public string SenderId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public FriendshipStatus Status { get; set; }

        public UserProfile SenderProifle { get; set; } = null!;
        public UserProfile TargetProfile { get; set; } = null!;
    }

    public enum FriendshipStatus { NotAccepted, Friend, BestFriend, Crush }
}
