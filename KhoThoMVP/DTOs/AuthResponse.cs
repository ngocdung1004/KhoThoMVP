﻿namespace KhoThoMVP.DTOs
{
    public class AuthResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserType { get; set; }
        public string Token { get; set; }
    }
}