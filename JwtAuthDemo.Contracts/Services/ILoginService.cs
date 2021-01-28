using JwtAuthDemo.Entities;

namespace JwtAuthDemo.Contracts.Services
{
    public interface ILoginService
    {
        User UserLogin(UserLoginDetail userLogin);
    }
}
