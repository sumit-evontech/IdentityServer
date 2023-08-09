using Auth.Utils.CustomErrorHandling;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Utils.JWT
{
    public class JWTToken : IJWTToken
    {
        public string GenerateToken(string username, string secretKey, string issuer, string audience, int expirationMinutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ExtractUsernameFromToken(string jwtToken, string secretKey, string issuer, string audience)
        {
            try
            {
                jwtToken = jwtToken.Split(' ')[1];
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(secretKey);
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);

                Claim? usernameClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    return usernameClaim.Value;
                }
                else
                {
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Username claim is missing");
                }
            }
            catch (SecurityTokenValidationException ex)
            {
                // Handle token validation errors, such as invalid signature, expired token, etc.
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors, if any.
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }

}
