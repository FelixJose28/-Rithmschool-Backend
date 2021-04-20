using AutoMapper;
using Rithmschool.Core.DTOs;
using Rithmschool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rithmschool.Infrastructure.Mappings
{
    public class MapperEntities: Profile
    {
        public MapperEntities()
        {
            CreateMap<Buy,BuyDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Teacher, TeacherDTO >().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
