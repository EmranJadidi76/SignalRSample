using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsRemember { get; set; }

    }
}
