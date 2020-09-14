using AutoMapper;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.EntitiesContext.UserContext.Commands;
using Messenger.Domain.Entities;

namespace Messenger.Application.Options.AutoMapper
{
    public class AutoMappingProfile: Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<CreateUser, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}