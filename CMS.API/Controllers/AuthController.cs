using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Application.Services;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS.API.Controllers
{
    //[Authorize]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await _authService.Authenticate(loginReq.Username, loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var loginRes = new LoginResDto();
            loginRes.Username = user.Email;
            loginRes.Token = CreateJWT(user);
            //loginRes.Token = "Token to be generated";
            return Ok(loginRes);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterReqDto registerReqDto)
        {
            if (await _authService.CustAlreadyExists(registerReqDto.Email))
                return StatusCode(409); //BadRequest("Email already exists, please try something else");

            var registerResDto = await _authService.Register(registerReqDto);
            if(registerResDto == null) 
                BadRequest("Email already exists, please try something else");

            return StatusCode(201);
        }
        private string CreateJWT(Customer custDto)
        {
            var secretKey = configuration.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                //.GetBytes("shhh.. this is my top secret"));
                .GetBytes(secretKey));

            var claims = new Claim[] {
            new Claim(ClaimTypes.Name,custDto.Email),
            new Claim(ClaimTypes.NameIdentifier,custDto.CustomerId.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
