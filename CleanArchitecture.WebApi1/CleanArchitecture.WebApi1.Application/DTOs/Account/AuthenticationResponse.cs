using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CleanArchitecture.WebApi1.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }

        public string  Phone { get; set; }

        public string  NickName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }

        public byte[] ImageByte { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
