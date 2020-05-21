using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API;
using Application.Contexts.Users.Commands;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Tests
{
    [TestClass]
    public class Users : BaseClass
    {
        private CreateUser validUserRequest = new CreateUser()
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@test.com",
            Password = "testPassword#123"
        };
        
        private CreateUser userWithInvalidPassword = new CreateUser()
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@test.com",
            Password = "testPassword123"
        };
        
        private CreateUser userWithInvalidEmail = new CreateUser()
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@test",
            Password = "testPassword#123"
        };
        
        
        [TestMethod]
        public async Task CreateUserSuccessful()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(validUserRequest), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task CreateUserWithInvalidPasswordShouldReturnBadReques()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(userWithInvalidPassword), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateUserWithInvalidEmailShouldReturnBadRequest()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(userWithInvalidEmail), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}