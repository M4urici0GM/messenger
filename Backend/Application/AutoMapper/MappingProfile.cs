using Application.Contexts.Users.Commands;
using Application.DataTransferObjects;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUser, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }   
    }
}