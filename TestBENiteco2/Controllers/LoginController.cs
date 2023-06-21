using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestBENiteco2.Models;
using TestBENiteco2.Request;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }


    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login(UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        if (user != null)
        {
            var token = GenerateToken(user);
            return Ok(new
            {
                userName = user.Username,
                role = user.Role,
                token = token,
            });
        }

        return BadRequest(new { Message = "user not found", Code = "ER_02" });
    }

    // To generate token
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //To authenticate user
    private User Authenticate(UserLogin userLogin)
    {
        var currentUser = UserContants.Users.FirstOrDefault(x =>
            String.Equals(x.Username, userLogin.Username, StringComparison.CurrentCultureIgnoreCase) &&
            x.Password == userLogin.Password);
        return currentUser ?? null;
    }
}