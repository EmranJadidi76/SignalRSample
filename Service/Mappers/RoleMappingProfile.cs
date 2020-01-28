using AutoMapper;
using DataLayer.DTO.RolesDTO;
using DataLayer.DTO.UserDTO;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class RoleMappingProfile:Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Roles, RoleManageDTO>().ReverseMap();
            CreateMap<Roles, RolesDTO>().ReverseMap();
        }
    }
}
