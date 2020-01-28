using DataLayer.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Users
{
    public class Roles : IdentityRole<int>, IEntity
    {
        public string RoleTitle { get; set; }


    }
}
