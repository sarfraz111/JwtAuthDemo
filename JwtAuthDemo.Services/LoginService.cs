using JwtAuthDemo.Contracts.DA;
using JwtAuthDemo.Contracts.Security;
using JwtAuthDemo.Contracts.Services;
using JwtAuthDemo.Entities;
using System;

namespace JwtAuthDemo.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo repo;
        private readonly IJwtToken jwtToken;
        public LoginService(ILoginRepo repo, IJwtToken jwtToken)
        {
            this.repo = repo;
            this.jwtToken = jwtToken;
        }
        public User UserLogin(UserLoginDetail userLogin)
        {
            var user = repo.UserLogin(userLogin);
            if (user != null)
            {
                user.Token = jwtToken.GetToken(user);
            }
            return user;
        }
    }
}
