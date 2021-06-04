using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Tests.Integration
{
    public class UserIntegrationTest : BaseIntegrationTest
    {
        public UserIntegrationTest()
        {
        }

        public async Task<UserResultDto> CreateUser()
        {
            var userCreateDto = new UserCreateDto()
            {
                Name = Faker.Name.First(),
                Email = Faker.Internet.Email(),
                Password = "mudar@123"
            };

            await AdicionarToken();
            var response = await PostAsync(userCreateDto, "users");
            var postResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResultDto>(postResult);
        }

        [Fact(DisplayName = "Should List User")]
        [Trait("Integration", "User")]
        public async void ShouldListUser()
        {
            // Arrange

            // Assumption
            await AdicionarToken();
            var response = await GetAsync("users");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should Create User")]
        [Trait("Integration", "User")]
        public async void ShouldCreateUser()
        {
            // Arrange
            var userCreateDto = new UserCreateDto()
            {
                Name = Faker.Name.First(),
                Email = Faker.Internet.Email(),
                Password = "mudar@123"
            };

            // Assumption
            await AdicionarToken();
            var response = await PostAsync(userCreateDto, "users");
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserResultDto>(postResult);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(userCreateDto.Name, registroPost.Name);
            Assert.Equal(userCreateDto.Email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));
        }

        [Fact]
        [Trait("Integration", "User")]
        public async void ShouldNotCreateUser()
        {
            // Arrange
            var userCreateDto = new UserCreateDto() { Name = Faker.Name.First() };

            // Assumption
            await AdicionarToken();
            var response = await PostAsync(userCreateDto, "users");
            var postResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should Update User")]
        [Trait("Integration", "User")]
        public async void ShouldUpdateUser()
        {
            // Arrange
            var user = CreateUser().Result;
            var userUpdateDto = new UserUpdateDto()
            {
                Id = user.Id,
                Name = Faker.Name.FullName()
            };

            // Assumption
            await AdicionarToken();
            var response = await PutAsync(userUpdateDto, "users");
            var putResult = await response.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserResultDto>(putResult);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userUpdateDto.Name, updatedUser.Name);
            Assert.True(updatedUser.Id != default(Guid));
        }

        [Fact(DisplayName = "Should Not Update User")]
        [Trait("Integration", "User")]
        public async void ShouldNotUpdateUser()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto() { Name = Faker.Name.FullName() };

            // Assumption
            await AdicionarToken();
            var response = await PutAsync(userUpdateDto, "users");
            var putResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Should Delete User")]
        [Trait("Integration", "User")]
        public async void ShouldDeleteUser()
        {
            // Arrange
            var user = CreateUser().Result;

            // Assumption
            await AdicionarToken();
            var response = await DeleteAsync("users/" + user.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Should Not Delete User")]
        [Trait("Integration", "User")]
        public async void ShouldNotDeleteUser()
        {
            // Arrange
            var guid = new Guid();

            // Assumption
            await AdicionarToken();
            var response = await DeleteAsync("users/" + guid);
            var responseWithoutGuid = await DeleteAsync("users/");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, responseWithoutGuid.StatusCode);
        }
    }
}
