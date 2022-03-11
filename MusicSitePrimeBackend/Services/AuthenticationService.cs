using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicSitePrimeBackend.Configurations;
using MusicSitePrimeBackend.Domain.Models;

namespace MusicSitePrimeBackend.Services;

public interface IAuthenticationService
{
    string BuildTokenForUser(User user);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtConfiguration _configuration;
    
    public AuthenticationService(IOptions<JwtConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }
    
    public string BuildTokenForUser(User user)
    {
        var token_handler = new JwtSecurityTokenHandler();
        var key = _configuration.GetSecretInBytes();
        var token_descriptor = new SecurityTokenDescriptor 
        {
            Subject = new ClaimsIdentity( new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("secret", user.Secret),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("rights", string.Join(';', user.Rights))
            }),
            Expires = DateTime.UtcNow.AddHours(_configuration.HoursExpires),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token =  token_handler.CreateToken(token_descriptor);
        return token_handler.WriteToken(token);
    }
}