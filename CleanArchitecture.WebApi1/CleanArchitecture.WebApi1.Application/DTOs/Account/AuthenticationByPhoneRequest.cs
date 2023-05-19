using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.DTOs.Account
{
    public class AuthenticationByPhoneRequest
    {
        public string NickName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public byte[] ImageByte { get; set; }

        public byte[] ProfileImageByte { get; set; }

    }
}
