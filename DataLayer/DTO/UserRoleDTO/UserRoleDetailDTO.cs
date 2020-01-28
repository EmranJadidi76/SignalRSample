using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.UserRoleDTO
{
    public class UserRoleDetailDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
    }
}
