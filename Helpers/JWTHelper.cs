using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParagonID.InternalSystem.Helpers
{
    public class JWTHelper
    {
        /// <summary>
        /// This is the function that creates the JWT.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string userId)
        {
            string jwtSecretToken = Environment.GetEnvironmentVariable("JwtSecretToken")!;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecretToken);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
