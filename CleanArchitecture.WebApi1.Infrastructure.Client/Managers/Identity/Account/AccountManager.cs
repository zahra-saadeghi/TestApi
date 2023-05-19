using CleanArchitecture.WebApi1.Application.DTOs.Account;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Infrastructure.Client.Extensions;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;


namespace CleanArchitecture.WebApi1.Infrastructure.Client.Managers.Identity.Account
{
    public static class AccountEndpoints
    {
        public static string Login = "api/account/authenticate";

        public static string LoginByPhone = "api/account/authenticateByPhone";

        public static string TwoFactorSignIn = "api/account/twoFactorSignIn";

        public static string GetAllCustomer = "api/account/getAll-Customer";

        public static string GetAllUser = "api/account/getAll-User";

        public static string EditUserProfile = "api/account/Edit-UserProfile";
        
    }
    public class AccountManager : IAccountManager
    {
        private readonly HttpClient _httpClient;

        public AccountManager()
        {
            _httpClient = new HttpClient();

        }

        public AccountManager(HttpClient httpClient)
        {
            _httpClient = httpClient;



        }

        //public async Task<ClaimsPrincipal> CurrentUser()
        //{
        //    var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        //    return state.User;
        //}




        public async Task<Result<string>> LoginByphone(AuthenticationByPhoneRequest model)
        {
            var a = AccountEndpoints.LoginByPhone.ToFullUrl();
            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.LoginByPhone.ToFullUrl(), model);
            var result = await response.ToResult<string>();
            if (result.Succeeded)
            {
                var userId = result.Data;

                return await Result<string>.SuccessAsync(result.Data);
            }
            else
            {
                return await Result<string>.FailAsync(result.Messages);
            }
        }

        public async Task<IResult<AuthenticationResponse>> TwoFactorSignIn(TwoFactorSignInRequest model)
        {

            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.TwoFactorSignIn.ToFullUrl(), model);
            var result = await response.ToResult<AuthenticationResponse>();
            if (result.Succeeded)
            {
                return await Result<AuthenticationResponse>.SuccessAsync(result.Data);
            }
            else
            {
                return await Result<AuthenticationResponse>.FailAsync(result.Messages);
            }
        }




        public async Task<Result<AuthenticationResponse>> Login(AuthenticationRequest model)
        {

            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.Login.ToFullUrl(), model);
            var result = await response.ToResult<AuthenticationResponse>();
            if (result.Succeeded)
            {
                var token = result.Data.JWToken;
                var refreshToken = result.Data.RefreshToken;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Result<AuthenticationResponse>.SuccessAsync(result.Data);
            }
            else
            {
                return await Result<AuthenticationResponse>.FailAsync(result.Messages);
            }
        }

        public async Task<IResult<List<ApplicationUserRequest>>> GetAllCustomer()
        {
            var response = await _httpClient.GetAsync(AccountEndpoints.GetAllCustomer.ToFullUrl());
            return await response.ToResult<List<ApplicationUserRequest>>();
        }

        public async Task<IResult<List<ApplicationUserRequest>>> GetAllUser()
        {
            var response = await _httpClient.GetAsync(AccountEndpoints.GetAllUser.ToFullUrl());
            return await response.ToResult<List<ApplicationUserRequest>>();
        }


        public async Task<IResult<bool>> EditUserProfile(EditUserProfileRequest model)
        {

            var response = await _httpClient.PostAsJsonAsync(AccountEndpoints.EditUserProfile.ToFullUrl(), model);
            var result = await response.ToResult<bool>();
            if (result.Succeeded)            
                 return await Result<bool>.SuccessAsync(result.Data);            
            else            
                return await Result<bool>.FailAsync(result.Messages);
            
        }
    }
}