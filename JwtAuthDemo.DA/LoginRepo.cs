using JwtAuthDemo.Contracts.DA;
using JwtAuthDemo.Entities;

namespace JwtAuthDemo.DA
{
    public class LoginRepo : ILoginRepo
    {
        public User UserLogin(UserLoginDetail userLogin)
        {
            return new User() { UserName = userLogin.UserId, Email = "sss@ss.com" };
        }
    }
}
