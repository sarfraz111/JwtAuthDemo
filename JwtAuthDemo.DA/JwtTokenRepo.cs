using JwtAuthDemo.Contracts.DA;
using System;

namespace JwtAuthDemo.DA
{
    public class JwtTokenRepo : IJwtTokenRepo
    {
        public void DeleteRefreshToken(string username, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public string GetRefreshToken(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveRefreshToken(string username, string newRefreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
