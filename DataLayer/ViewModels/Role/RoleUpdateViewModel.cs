using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Role
{
    /// <summary>
    /// ویرایش نقش
    /// </summary>
    public class RoleUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleTitle { get; set; }
    }
}
