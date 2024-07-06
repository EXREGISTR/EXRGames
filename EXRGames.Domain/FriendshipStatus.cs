namespace EXRGames.Domain {
    public class Friend {
        public Guid UserId { get; set; }
        public UserProfile UserProifle { get; set; } = null!;

        public Guid FriendId { get; set; }
        public UserProfile FriendProfile { get; set; } = null!;

        public FriendshipStatus Status { get; set; }
    }

    public enum FriendshipStatus { Pending, Accepted, Declined }
}
