﻿namespace Core.DTOs
{
    public class TokenJWT
    {
        public required string Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
