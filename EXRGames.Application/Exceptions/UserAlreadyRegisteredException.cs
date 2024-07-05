namespace EXRGames.Application.Exceptions {
    public class UserAlreadyRegisteredException(string login) 
        : Exception($"User with login {login} already registered!");
}
