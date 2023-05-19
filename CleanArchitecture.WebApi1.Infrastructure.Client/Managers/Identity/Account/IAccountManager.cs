

using CleanArchitecture.WebApi1.Application.DTOs.Account;
using CleanArchitecture.WebApi1.Application.Wrappers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<Result<AuthenticationResponse>> Login(AuthenticationRequest model);

        Task<Result<string>> LoginByphone(AuthenticationByPhoneRequest model);

        Task<IResult<AuthenticationResponse>> TwoFactorSignIn(TwoFactorSignInRequest model);

        Task<IResult<List<ApplicationUserRequest>>> GetAllCustomer();

        Task<IResult<List<ApplicationUserRequest>>> GetAllUser();

        Task<IResult<bool>> EditUserProfile(EditUserProfileRequest model);


    }
}