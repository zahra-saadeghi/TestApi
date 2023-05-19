using CleanArchitecture.WebApi1.Application.DTOs.Account;
using CleanArchitecture.WebApi1.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AccountController(IAccountService accountService, IHostingEnvironment hostingEnvironment)
        {
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
        
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("authenticateByPhone")]
        public async Task<IActionResult> AuthenticateByPhoneAsync(AuthenticationByPhoneRequest request)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Image", "profile.png");
            byte[] profileByte = System.IO.File.ReadAllBytes(htmlFilePath);
            request.ProfileImageByte = profileByte;
            return Ok(await _accountService.AuthenticateByPhoneAsync(request));
        }
        [HttpPost("twoFactorSignIn")]
        public async Task<IActionResult> TwoFactorSignInAsync(TwoFactorSignInRequest request)
        {
            return Ok(await _accountService.TwoFactorSignInAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        [HttpGet("getAll-Customer")]
        public async Task<IActionResult> GetAllCustomer()
        {

            return Ok(await _accountService.GetAllCustomer());
        }

        [HttpGet("getAll-User")]
        public async Task<IActionResult> GetAllUser()
        {

            return Ok(await _accountService.GetAllUser());
        }

        [HttpPost("Edit-UserProfile")]
        public async Task<IActionResult> EditUserProfile(EditUserProfileRequest request)
        {
            return Ok(await _accountService.EditUserProfile(request));
        }
    }
}