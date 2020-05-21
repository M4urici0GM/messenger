using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        
        public ErrorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (EntityNotFoundException e)
            {
                await Response(context, HttpStatusCode.NotFound, new {message = e.Message});
            }
            catch (EntityValidationException e)
            {
                await Response(context, HttpStatusCode.BadRequest, new {errors = e.Failures});
            }
            catch (EntityAlreadyExists e)
            {
                await Response(context, HttpStatusCode.Conflict, new {message = e.Message});
            }
            catch (InvalidCredentialException e)
            {
                await Response(context, HttpStatusCode.Unauthorized, new {message = e.Message});
            }
            catch (Exception e)
            {
                //TODO: Yet to implement database error logging.
                await Response(context, HttpStatusCode.InternalServerError, new { });
            }
        }

        private static Task Response(HttpContext context, HttpStatusCode statusCode, object content)
        {
            string responseContent = JsonConvert.SerializeObject(content);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            return context.Response.WriteAsync(responseContent);
        }
    }
}