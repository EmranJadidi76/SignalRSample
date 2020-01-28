using DataLayer.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Users
{
    public class UserClams : IdentityUserClaim<int>, IEntity
    {
    }
}
