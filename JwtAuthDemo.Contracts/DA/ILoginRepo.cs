using JwtAuthDemo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthDemo.Contracts.DA
{
    public interface ILoginRepo
    {
        User UserLogin(UserLoginDetail userLogin);
    }
}
