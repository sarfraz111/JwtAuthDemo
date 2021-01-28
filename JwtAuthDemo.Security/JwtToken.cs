using JwtAuthDemo.Contracts.Security;
using JwtAuthDemo.Contracts.Services;
using JwtAuthDemo.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthDemo.Security
{
    public class JwtToken : IJwtToken
    {
        private readonly string jwtSignedSecurityKey;
        private readonly IJwtTokenService jwtTokenService;
        public JwtToken(string jwtSignedSecurityKey, IJwtTokenService jwtTokenService)
        {
            this.jwtSignedSecurityKey = jwtSignedSecurityKey;
            this.jwtTokenService = jwtTokenService;
        }

        public Tuple<string, string> Refresh(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = jwtTokenService.GetRefreshToken(username); //retrieve the refresh token from a data store
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = GenerateToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            jwtTokenService.DeleteRefreshToken(username, refreshToken);
            jwtTokenService.SaveRefreshToken(username, newRefreshToken);

            return Tuple.Create(
                token = newJwtToken,
                refreshToken = newRefreshToken
            );
        }

        public string GetToken(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Country, user.Country),
                new Claim(ClaimTypes.StateOrProvince, user.StateOrProvince),
                new Claim(ClaimTypes.StreetAddress, user.StreetAddress),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
             };
            return GenerateToken(claims);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSignedSecurityKey));

            var jwt = new JwtSecurityToken(issuer: "Everyone",
                audience: "Everyone",
                claims: claims, //the user's claims, for example new Claim[] { new Claim(ClaimTypes.Name, "The username"), //... 
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSignedSecurityKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
