namespace EXRGames.Domain {
    public class UserRelationship {
        public string SenderId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public RelationshipStatus? Status { get; set; }

        public UserProfile SenderProifle { get; set; } = null!;
        public UserProfile TargetProfile { get; set; } = null!;
    }

    public enum RelationshipStatus { Friend, BestFriend, Crush }
}
