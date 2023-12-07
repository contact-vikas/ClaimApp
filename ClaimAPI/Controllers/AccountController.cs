using ClaimAPI.Models;
using ClaimAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService _accountService { get; }
        private APIResponse response = new APIResponse();
        private IConfiguration _config;
        public AccountController(IAccountService account, IConfiguration _config)
        {
            _accountService = account;
            this._config = _config;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLogin user)
        {
            try
            {
                var result = _accountService.Login(user);
                if (result == 1)
                {
                    var userInfo = _accountService.GetUserByEmail(user.Email);
                    response.status = 200;
                    response.ok = true;
                    response.data = userInfo;
                    response.message = "User authenticated successfully!";

                    response.token = GenerateJSONWebToken(userInfo);

                }
                else if (result == 2)
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "User not autheticated!";
                }
                else if (result == 3)
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "User not exist!";
                }
                else
                {
                    response.status = 101;
                    response.ok = false;
                    response.data = null;
                    response.message = "Something went wrong!";
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.ok = false;
                response.data = null;
                response.message = ex.Message;
            }

            return Ok(response);
        }

      
        private string GenerateJSONWebToken(UserVm userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
