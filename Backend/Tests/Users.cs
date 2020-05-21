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
        [TestMethod]
        public async Task CreateUserSuccessful()
        {
            CreateUser createUserRequest = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Password = "testPassword#123"
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(createUserRequest), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task CreateUserWithInvalidPasswordShouldReturnBadReques()
        {
            CreateUser createUserRequest = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Password = "testPassword123"
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(createUserRequest), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateUserWithInvalidEmailShouldReturnBadRequest()
        {
            CreateUser createUserRequest = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test",
                Password = "testPassword#123"
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(createUserRequest), 
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync("api/user", content);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}