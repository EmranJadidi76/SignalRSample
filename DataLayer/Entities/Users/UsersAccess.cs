using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Users
{
    public class UsersAccess : BaseEntity<int>
    {
        //public int UserId { get; set; }

        public int RoleId { get; set; }

        public string Controller { get; set; }

        public string Actions { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Roles Roles { get; set; }
    }
}
