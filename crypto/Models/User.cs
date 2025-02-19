using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; }
        public string? HashPassword { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "User"; // Default role

        //Refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}