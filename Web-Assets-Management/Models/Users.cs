using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Assets_Management.Models
{
    public class Users
    {
        public int EId { get; set; }
        public string Password { get; set; }
        public string Auth { get; set; }
        public Users(int id, string password, string auth)
        {
            EId = id;
            Password = password;
            Auth = auth;
        }
    }
}
