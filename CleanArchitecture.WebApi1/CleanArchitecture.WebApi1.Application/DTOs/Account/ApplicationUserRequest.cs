using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.DTOs.Account
{
    public class ApplicationUserRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceManufacturer { get; set; }

        public string DeviceName { get; set; }

        public string DeviceOSVersion { get; set; }

        public string DeviceIdiom { get; set; }

        public string DevicePlatform { get; set; }
    }

}
