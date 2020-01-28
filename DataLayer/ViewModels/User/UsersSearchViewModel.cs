using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class UsersSearchViewModel
    {

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? CreateDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? IsActive { get; set; }

    }
}
