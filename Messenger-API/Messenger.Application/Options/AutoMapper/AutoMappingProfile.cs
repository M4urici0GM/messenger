using AutoMapper;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.EntitiesContext.MessageContext.Commands;
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
            CreateMap<CreateMessage, Message>();
        }
    }
}