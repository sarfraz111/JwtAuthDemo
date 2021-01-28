using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthDemo.Contracts.Services;
using JwtAuthDemo.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JwtAuthDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IConfiguration configuration;
        public LoginController(ILoginService loginService, IConfiguration _configuration)
        {
            this.loginService = loginService;
            this.configuration = _configuration;
        }
        public IActionResult UserLogin(UserLoginDetail userLogin)
        {
            return Ok(loginService.UserLogin(userLogin));
        }
    }
}
