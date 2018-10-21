using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authrepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authrepository, IConfiguration configuration)
    {
        _authrepository = authrepository;
        _configuration = configuration;

        }
    [HttpPost("register")]
    public async Task<IActionResult>Register(UserForRegisterDto user)
    {
        user.UserName = user.UserName.ToLower();
        if(await _authrepository.UserExists(user.UserName))
        return BadRequest("Username Exists");
        
        var newUser = new User
        {
            UserName = user.UserName
        };
        var createdUser = await _authrepository.Register(newUser, user.UserPassword);
        return Ok("User Created");

    }

    [HttpPost("login")]
    public async Task<IActionResult>Login(UserLoginDto userLoginDto)

    {
        var validUser = await _authrepository.Login(userLoginDto.Username.ToLower(), userLoginDto.Password);
        if(validUser ==null)
        return Unauthorized();

        var claims = new[] 
        {
            new Claim(ClaimTypes.NameIdentifier, validUser.Id.ToString()),
            new Claim(ClaimTypes.Name, validUser.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor 
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds

        };
        var tokenHandlers = new JwtSecurityTokenHandler();
        var token = tokenHandlers.CreateToken(tokenDescriptor);

        return Ok(new {
           token =  tokenHandlers.WriteToken(token)
        });

    }




}
}