using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.DTOs.Account
{
    public class TwoFactorSignInRequest
    {
        public string PhoneNumber { get; set; }
        public string TwoFactorCode { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceManufacturer { get; set; }

        public string DeviceName { get; set; }

        public string DeviceOSVersion { get; set; }

        public string DeviceIdiom { get; set; }

        public string DevicePlatform { get; set; }


    }
}
