using CleanArchitecture.WebApi1.Application.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceManufacturer { get; set; }

        public string DeviceName { get; set; }

        public string DeviceOSVersion { get; set; }

        public string DeviceIdiom { get; set; }

        public string DevicePlatform  { get; set; }

        public byte[] ImageByte { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
