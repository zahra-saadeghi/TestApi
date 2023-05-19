using AutoMapper;


using CleanArchitecture.WebApi1.Application.DTOs;
using CleanArchitecture.WebApi1.Application.DTOs.Account;
using CleanArchitecture.WebApi1.Application.DTOs.Email;
using CleanArchitecture.WebApi1.Application.Enums;
using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces;
using CleanArchitecture.WebApi1.Application.Statics;
using CleanArchitecture.WebApi1.Application.Wrappers;
using CleanArchitecture.WebApi1.Domain.Entities;
using CleanArchitecture.WebApi1.Domain.Settings;
using CleanArchitecture.WebApi1.Infrastructure.Identity.Helpers;
using CleanArchitecture.WebApi1.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.Infrastructure.Identity.Services
{

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;

        }

        public async Task<IResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return await Result<AuthenticationResponse>.FailAsync($"No Accounts Registered with {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return await Result<AuthenticationResponse>.FailAsync($"Invalid Credentials for '{request.Email}'.");

            }
            if (!user.EmailConfirmed)
            {
                return await Result<AuthenticationResponse>.FailAsync($"Account Not Confirmed for '{request.Email}'.");

            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.NickName = user.NickName;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.ImageByte = user.ImageByte;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return await Result<AuthenticationResponse>.SuccessAsync(data: response);

        }

        public async Task<Response<string>> AuthenticateByPhoneAsync(AuthenticationByPhoneRequest request)
        {

            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {

                //register user
                user = new ApplicationUser
                {
                    NickName = request.NickName,
                    PhoneNumber = request.PhoneNumber,
                    UserName = request.PhoneNumber,
                    Email = request.Email,
                    TwoFactorEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    ImageByte = request.ProfileImageByte
                };

                var result = await _userManager.CreateAsync(user, StaticValues.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                    await _signInManager.SignOutAsync();
                    user = await _userManager.FindByNameAsync(request.PhoneNumber);
                    var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");

                    await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { To = user.Email, Body = $"OTP Code: {code}", Subject = "OTP Code" });
                    return new Response<string>(user.Id, message: $"Please check email address {user.Email} and enter OTP code for login");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
            else
            {
                if (user.NickName != request.NickName)
                {
                    user.NickName = request.NickName;
                    await _userManager.UpdateAsync(user);
                }
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");
                await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { To = user.Email, Body = $"OTP Code: {code}", Subject = "OTP Code" });
                return new Response<string>(user.Id, message: $"Please check email address {user.Email} and enter OTP code for login");
            }

        }

        public async Task<IResult<AuthenticationResponse>> TwoFactorSignInAsync(TwoFactorSignInRequest request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.PhoneNumber}.");
            }
            else
            {
                user.DevicePlatform = request.DevicePlatform;
                user.DeviceOSVersion = request.DeviceOSVersion;
                user.DeviceName = request.DeviceName;
                user.DeviceModel = request.DeviceModel;
                user.DeviceManufacturer = request.DeviceManufacturer;
                user.DeviceIdiom = request.DeviceIdiom;

                await _userManager.UpdateAsync(user);
            }

            var TwoFaResult = await _userManager.VerifyTwoFactorTokenAsync(user, "Phone", request.TwoFactorCode);

            if (TwoFaResult)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, StaticValues.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new ApiException($"Invalid Credentials for '{request.PhoneNumber}'.");
                }
                if (!user.PhoneNumberConfirmed)
                {
                    throw new ApiException($"Account Not Confirmed for '{request.PhoneNumber}'.");
                }
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.NickName = user.NickName;
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.UserName = user.UserName;
                response.Phone = user.PhoneNumber;
                response.ImageByte = user.ImageByte;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                response.IsVerified = user.EmailConfirmed;
                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;
                return await Result<AuthenticationResponse>.SuccessAsync(data: response);
            }
            else
            {
                throw new ApiException($"Account Not Confirmed for '{request.PhoneNumber}'.");
            }

        }

        public async Task<IResult<bool>> EditUserProfile(EditUserProfileRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Phone);
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }
            else
            {
                user.NickName = request.NickName;
                user.Email = request.Email;
                user.ImageByte = request.ImageByte != null && request.ImageByte.Length > 0 ? request.ImageByte : null;

                await _userManager.UpdateAsync(user);

                //  await _userManager.SetEmailAsync(user, user.Email);
                return await Result<bool>.SuccessAsync(true);
            }

        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                    var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email} is already registered.");
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = "api/account/reset-password/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new EmailRequest()
            {
                Body = $"You reset token is - {code}",
                To = model.Email,
                Subject = "Reset Password",
            };
            await _emailService.SendAsync(emailRequest);
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>(model.Email, message: $"Password Resetted.");
            }
            else
            {
                throw new ApiException($"Error occured while reseting the password.");
            }
        }

        public async Task<IResult<List<ApplicationUserRequest>>> GetAllCustomer()
        {
            var role = await _roleManager.FindByIdAsync(Roles.Customer.ToString());
            var users = await _userManager.GetUsersInRoleAsync(Roles.Customer.ToString());

            var result = users.Select(p => new ApplicationUserRequest
            {
                Id = p.Id,
                DeviceIdiom = p.DeviceIdiom,
                DeviceManufacturer = p.DeviceManufacturer,
                DeviceModel = p.DeviceModel,
                DeviceName = p.DeviceName,
                DeviceOSVersion = p.DeviceOSVersion,
                DevicePlatform = p.DevicePlatform,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                NickName = p.NickName,
                PhoneNumber = p.PhoneNumber,
                RefreshTokens = p.RefreshTokens,
                UserName = p.UserName
            }).ToList();

            return await Result<List<ApplicationUserRequest>>.SuccessAsync(result);

        }

        public async Task<IResult<List<ApplicationUserRequest>>> GetAllUser()
        {

            var users = _userManager.Users;

            var result = users.Select(p => new ApplicationUserRequest
            {
                Id = p.Id,
                DeviceIdiom = p.DeviceIdiom,
                DeviceManufacturer = p.DeviceManufacturer,
                DeviceModel = p.DeviceModel,
                DeviceName = p.DeviceName,
                DeviceOSVersion = p.DeviceOSVersion,
                DevicePlatform = p.DevicePlatform,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                NickName = p.NickName,
                PhoneNumber = p.PhoneNumber,
                RefreshTokens = p.RefreshTokens,
                UserName = p.UserName
            }).ToList();

            return await Result<List<ApplicationUserRequest>>.SuccessAsync(result);

        }
    }

}
