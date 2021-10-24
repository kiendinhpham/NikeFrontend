using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFrontend.Data
{
    
    public class UserRootobject
    {
        public UserData data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class UserData
    {
        public User user { get; set; }
        public string token { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
    }

}
