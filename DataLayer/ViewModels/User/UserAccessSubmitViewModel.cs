using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class UserAccessSubmitViewModel
    {
        public int RoleId { get; set; }
        public List<string> Actions { get; set; }

        public string Controller { get; set; }
    }
}
