﻿namespace EXRGames.Application.AccountRequests {
    public class LoginRequest {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}