using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class UserAccessListViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
