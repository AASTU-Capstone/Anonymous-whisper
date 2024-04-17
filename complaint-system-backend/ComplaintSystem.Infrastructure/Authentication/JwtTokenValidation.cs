using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Infrastructure.Authentication;
public class JwtTokenValidation : IJwtTokenValidation
{
    public readonly Jwtsettings _jwtsettings;
    public JwtTokenValidation(IOptions<Jwtsettings> jwtsettings)
    {
        _jwtsettings = jwtsettings.Value;
    }
    public Guid ExtractUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "NameId");
        if (userId == null)
        {
            return Guid.Empty;
        }
        return Guid.Parse(userId.Value);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenParamaters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),

            ValidateAudience = true,
            ValidAudience  = Environment.GetEnvironmentVariable("Audience"),

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),

            ClockSkew = TimeSpan.Zero,

        };

        try
        {
            SecurityToken validatedToken;
            tokenHandler.ValidateToken(token,tokenParamaters,out validatedToken);
            return true;
        }catch (Exception ex)
        {
            return false;
        }
    }
}
