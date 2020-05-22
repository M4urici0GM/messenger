using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API;
using Application.Contexts.Users.Commands;
using Application.DataTransferObjects;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;

namespace IntegrationTests.Controllers
{
    public class UserControllerTests : IClassFixture<TestApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;
        private readonly TestApplicationFactory<Startup> _factory;

        public UserControllerTests(TestApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }
        

        [Fact]
        public async Task CreateUser_WithValidUser()
        {
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test1@email.com",
                Password = "testPassword#123"
            };
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");
            
            // Act
            HttpResponseMessage httpResponse = await _client.PutAsync("api/user", requestContent);
            HttpResponseMessage newHttpResponse = await _client.PutAsync("api/user", requestContent);
            
            // Assert
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK && newHttpResponse.StatusCode == HttpStatusCode.Conflict);
        }
        
        [Fact]
        public async Task CreateUser_WithInvalidPassword_ReturnsBadRequest()
        {
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test2@test.com",
                Password = "invalidPassword"
            };
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");
            
            // Act
            var response = await _client.PutAsync("api/user", requestContent);
            
            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task CreateUser_WithInvalidEmail_ReturnsBadRequest()
        {
            // Arrange
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "invalid@email",
                Password = "testPassword#123"
            };
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");
            
            // Act
            var response = await _client.PutAsync("api/user", requestContent);
            
            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUser_WithoutPassword_ReturnsOK()
        {
            // Arrange
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "valid3@email.com",
                Password = "testPassword#123"
            };
            UpdateUserInfo updateUserInfo = new UpdateUserInfo();
            
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage createUserResponse = await _client.PutAsync("api/user", requestContent);
            
            string createdUserSerialized = await createUserResponse.Content.ReadAsStringAsync();
            User createdUser = JsonConvert.DeserializeObject<User>(createdUserSerialized);

            createdUser.Id.Should().NotBe(Guid.Empty);
            
            updateUserInfo.FirstName = "New User Name";
            updateUserInfo.LastName = "New User Last Name";
            updateUserInfo.UserId = createdUser.Id;
            
            var updateUserContent = new StringContent(
                JsonConvert.SerializeObject(updateUserInfo),
                Encoding.UTF8, "application/json");

            HttpResponseMessage updateUserResponse = await _client.PostAsync("api/user", updateUserContent);
            
            // Assert
            Assert.True(updateUserResponse.StatusCode == HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task UpdateUser_WithValidPassword_ReturnsOK()
        {

            using (var client = _factory.CreateClient())
            {
                // Arrange
                CreateUser user = new CreateUser()
                {
                    FirstName = "Test",
                    LastName = "User",
                    Email = "valid4@email.com",
                    Password = "testPassword#123"
                };
                UpdateUserInfo updateUserInfo = new UpdateUserInfo();
            
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json");

                // Act
                HttpResponseMessage createUserResponse = await client.PutAsync("api/user", requestContent);
            
                string createdUserSerialized = await createUserResponse.Content.ReadAsStringAsync();
                User createdUser = JsonConvert.DeserializeObject<User>(createdUserSerialized);
            
                updateUserInfo.FirstName = "New User Name";
                updateUserInfo.LastName = "New User Last Name";
                updateUserInfo.CurrentPassword = "testPassword#123";
                updateUserInfo.NewPassword = "newPassword#1234";
                updateUserInfo.UserId = createdUser.Id;
            
            
                var updateUserContent = new StringContent(
                    JsonConvert.SerializeObject(updateUserInfo),
                    Encoding.UTF8, "application/json");

                HttpResponseMessage updateUserResponse = await client.PostAsync("api/user", updateUserContent);
            
                // Assert
                updateUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            
        }
    }
}