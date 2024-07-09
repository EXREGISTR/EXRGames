namespace EXRGames.Application.Exceptions.Account {
    public class UserCouldntBeDeletedException(string reason) : Exception {
        public string Reason { get; } = reason;
    }
}
