namespace Auth.Utils.JWT
{
    public interface IJWTToken
    {
        public string GenerateToken(string username, string secretKey, string issuer, string audience, int expirationMinutes);
        public string ExtractUsernameFromToken(string jwtToken, string secretKey, string issuer, string audience);
    }
}
