using System;
using Application.DataTransferObjects;
using MediatR;

namespace Application.Contexts.Users.Queries
{
    public class GetUser : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
        
    }
}