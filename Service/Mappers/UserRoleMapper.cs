using AutoMapper;
using DataLayer.DTO;
using DataLayer.DTO.UserRoleDTO;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class UserRoleMapper : Profile
    {
        public UserRoleMapper()
        {

            CreateMap<UserRoles, UserRoleDetailDTO>();

        }
    }
}
