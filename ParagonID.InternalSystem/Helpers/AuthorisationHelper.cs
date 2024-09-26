using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;

namespace ParagonID.InternalSystem.Helpers
{
    public class AuthorisationHelper
    {
        // [Injections]
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        //

        public async Task<bool> IsAuthorized(string jwt)
        {
            if (!string.IsNullOrEmpty(jwt))
            {
                bool __validationResult = ValidateJWTSignature(jwt);
                return __validationResult;
            }

            return false;
        }

        private bool ValidateJWTSignature(string jwt)
        {
            var __tokenHandler = new JwtSecurityTokenHandler();
            var __validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecretToken")!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                SecurityToken validatedToken;
                var claimsPrincipal = __tokenHandler.ValidateToken(jwt, __validationParameters, out validatedToken);
                return true;
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
        }
    }
}
