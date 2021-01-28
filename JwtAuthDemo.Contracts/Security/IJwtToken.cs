using JwtAuthDemo.Entities;
using System;

namespace JwtAuthDemo.Contracts.Security
{
    public interface IJwtToken
    {
        Tuple<string, string> Refresh(string token, string refreshToken);
        string GetToken(User user);
    }
}
