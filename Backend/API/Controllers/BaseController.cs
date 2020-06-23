using System;
using API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly string _userId;
        
        protected readonly IMediator Mediator;
        protected readonly IHttpContextAccessor HttpContext;
        
        protected readonly Guid UserId;

        public BaseController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            Mediator = mediator;
            HttpContext = httpContextAccessor;
            _userId = HttpContext.HttpContext.User?.Identity.GetUserId();
            if (!string.IsNullOrEmpty(_userId))
                UserId = Guid.Parse(_userId);
        }
        
    }
}