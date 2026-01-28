using ClassInsight.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassInsight.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Autentica um usuário e gera o Token de acesso.
    /// </summary>
    /// <remarks>
    /// Use as credenciais de teste:
    /// - **Admin:** admin@escola.com / 123456
    /// - **Professor:** prof@escola.com / 123456
    /// </remarks>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        string role = "";
        if (request.Email == "admin@escola.com" && request.Password == "123456") role = "Admin";
        else if (request.Email == "prof@escola.com" && request.Password == "123456") role = "Professor";
        
        if (string.IsNullOrEmpty(role))
            return Unauthorized("Credenciais inválidas.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("Chave_Super_Secreta_Para_O_Tutorial_ClassInsight_Community_2026");
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { 
                new Claim(ClaimTypes.Name, request.Email), 
                new Claim(ClaimTypes.Role, role) 
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new AuthResponse(tokenHandler.WriteToken(token), request.Email, role));
    }
}