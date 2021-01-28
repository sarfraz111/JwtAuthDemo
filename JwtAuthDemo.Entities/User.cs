using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthDemo.Entities
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string StateOrProvince { get; set; }
        public string StreetAddress { get; set; }
        public string Token { get; set; }
    }
}
