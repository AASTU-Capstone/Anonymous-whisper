using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Persistence.Contracts.Common;
using  ComplaintSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    public readonly IDateTimeProvider _dateTimeProvider;
    public readonly Jwtsettings _jwtsettings;
    public JwtTokenGenerator(IOptions<Jwtsettings> jwtsettings, IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtsettings = jwtsettings.Value;
        
    }
    public string GenerateToken(UserEntity user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))), SecurityAlgorithms.HmacSha256Signature);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, user.User_Type),
            new Claim("userid", user.Id.ToString()),
            new Claim("useremail", user.Email)
        };

        var securityToken = new JwtSecurityToken(
                            issuer: Environment.GetEnvironmentVariable("Issuer"),
                            audience: Environment.GetEnvironmentVariable("Audience"),
                            claims: claims,
                            expires: _dateTimeProvider.CreateTime.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("Expiry_In_Minutes"))),
                            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

}