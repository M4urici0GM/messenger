using Messenger.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Messenger.Application.DataTransferObjects;
using Newtonsoft.Json;

namespace Messenger.Application.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Items.Add("RequestId", Guid.NewGuid());
                await _requestDelegate(httpContext);
            }
            catch (EntityNotFoundException ex)
            {
                await Response(httpContext, HttpStatusCode.NotFound, ex);
            }
            catch (EntityValidationException ex)
            {
                await Response(httpContext, HttpStatusCode.BadRequest, ex, ex.ValidationErrors.Select(x => new
                {
                    Name = x.PropertyName,
                    Message = x.ErrorMessage,
                }));
            }
            catch (InvalidCredentialException ex)
            {
                await Response(httpContext, HttpStatusCode.Unauthorized, ex);
            }
            catch (EntityAlreadyExistsException ex)
            {
                await Response(httpContext, HttpStatusCode.Conflict, ex);
            }
            catch (Exception ex)
            {
                await Response(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private Task Response(HttpContext context, HttpStatusCode statusCode, Exception exception, object response = null)
        {
            bool hasRequestId = context.Items.TryGetValue("RequestId", out var requestId);
            Guid? guidRequestId = (hasRequestId)
                ? (Guid) requestId
                : Guid.NewGuid();

            string responseContent = JsonConvert.SerializeObject(new ApiResponse
            {
                Message = exception.Message,
                Status = statusCode,
                RequestId = guidRequestId.Value,
                Response = response,
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            return context.Response.WriteAsync(responseContent);
        }
    }
}
