using DeliveryAppBackend.Extensions;
using DeliveryAppBackend.Features.Accounts.Models;
using DeliveryAppBackend.Services.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using Accounts.Models;

namespace DeliveryAppBackend.Features.Accounts.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private UserManager<SystemUser> _userManager;
        private IEmailSender _emailSender;
        private SignInManager<SystemUser> _signInManager;
        private RoleManager<AppRole> _roleManager;
        private IConfiguration _config;
        public AccountController(UserManager<SystemUser> userManager,
           IEmailSender emailSender,
           SignInManager<SystemUser> signInManager,
           RoleManager<AppRole> roleManager,
           IConfiguration config
            )
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        // POST api/account
        [HttpPost("register")]

        public async Task<IActionResult> Post([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new SystemUser {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName =registerViewModel.FirstName,
                    LastName= registerViewModel.LastName
                };
                var password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
                IdentityResult result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(registerViewModel.Email, callbackUrl, password);
                    var userViewModel = this.MapUserToViewModel(user);
                    return new OkObjectResult(userViewModel);
                }
            }

            return new BadRequestResult();

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("InvalidLogin", "User does not exist");
                    return new BadRequestObjectResult(ModelState);
                }
                if (!await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
                {
                    ModelState.AddModelError("InvalidLogin", "Username and  password do not match");
                    return new BadRequestObjectResult(ModelState);
                }
                var token = await BuildToken(user);
                return new OkObjectResult(token);
            }
            return new BadRequestObjectResult(ModelState);

        }

        [HttpGet("confirm-email", Name = "ConfirmEmail")]
        [AllowAnonymous]

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {

            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return new OkObjectResult(result.Succeeded ? "Email Confirmation successful" : "Error");
        }


        [HttpGet("reset-password")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        private async Task<object> BuildToken(SystemUser user)
        {

            var claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),


        
       // new Claim(JwtRegisteredClaimNames.)
        //new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var epiryTime = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: epiryTime,
              signingCredentials: creds);
            var authToken = new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = 60 * 60,
                UserDetails = new SystemUserViewModel
                {
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Id = user.Id,
                    UserName= user.Email,
                    FirstName=user.FirstName,
                    LastName=user.LastName,


                }
            };
            return authToken;
        }

        //public async Task<IActionResult> GetAllUsers() {
        //    this._roleManager.

        //}

        
        private SystemUserViewModel MapUserToViewModel(SystemUser user) {
            return new SystemUserViewModel {
                LastName= user.LastName,
                Email = user.Email,
                Id= user.Id,
                FirstName=user.FirstName,
                PhoneNumber= user.PhoneNumber,
                UserName = user.UserName,
            };
        }
    }
}