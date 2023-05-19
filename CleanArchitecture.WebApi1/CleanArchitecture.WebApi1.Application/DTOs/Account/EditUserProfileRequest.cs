using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApi1.Application.DTOs.Account
{
    public class EditUserProfileRequest
    {
        public string Email { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public byte[] ImageByte { get; set; }
    }
}
