using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Contexts.Users.Commands;
using Application.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class UserControllerTests : IntegrationTests
    {
        [Fact]
        public async Task CreateUser_WithValidUser()
        {
            // Arrange
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Password = "testPassword#123"
            };
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");
            
            // Act
            HttpResponseMessage httpResponse = await testClient.PutAsync("api/user", requestContent);
            HttpResponseMessage newHttpResponse = await testClient.PutAsync("api/user", requestContent);
            
            // Assert
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK && newHttpResponse.StatusCode == HttpStatusCode.Conflict);
        }
        
        [Fact]
        public async Task CreateUser_WithInvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            CreateUser user = new CreateUser()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Password = "invalidPassword"
            };
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");
            
            // Act
            var response = await testClient.PutAsync("api/user", requestContent);
            
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
            var response = await testClient.PutAsync("api/user", requestContent);
            
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
                Email = "valid@email.com",
                Password = "testPassword#123"
            };
            UpdateUserInfo updateUserInfo = new UpdateUserInfo();
            
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage createUserResponse = await testClient.PutAsync("api/user", requestContent);
            
            string createdUserSerialized = await createUserResponse.Content.ReadAsStringAsync();
            User createdUser = JsonConvert.DeserializeObject<User>(createdUserSerialized);

            Assert.True(createdUser.Id != null);
            
            updateUserInfo.FirstName = "New User Name";
            updateUserInfo.LastName = "New User Last Name";
            updateUserInfo.UserId = createdUser.Id;
            
            var updateUserContent = new StringContent(
                JsonConvert.SerializeObject(updateUserInfo),
                Encoding.UTF8, "application/json");

            HttpResponseMessage updateUserResponse = await testClient.PostAsync("api/user", updateUserContent);
            
            // Assert
            Assert.True(updateUserResponse.StatusCode == HttpStatusCode.OK);
        }
    }
}