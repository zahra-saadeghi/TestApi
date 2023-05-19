using CleanArchitecture.WebApi1.Application.DTOs.Account;
using CleanArchitecture.WebApi1.Application.Wrappers;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Application.Interfaces
{    
    public interface IAccountService
    {
        Task<IResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> AuthenticateByPhoneAsync(AuthenticationByPhoneRequest request);
        Task<IResult<AuthenticationResponse>> TwoFactorSignInAsync(TwoFactorSignInRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<IResult<List<ApplicationUserRequest>>> GetAllCustomer();

        Task<IResult<bool>> EditUserProfile(EditUserProfileRequest request);

        Task<IResult<List<ApplicationUserRequest>>> GetAllUser();
    }
}
