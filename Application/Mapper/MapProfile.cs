using Application.Dtos;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // CreateMap<Source, Destination>();

            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<RoleDto, Role>();
            CreateMap<string, Role>().ConstructUsing(name => new Role { Name = name, Id = Guid.NewGuid().ToString() });

            CreateMap<Product , ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductDto, Product>();
        }
    }
}
