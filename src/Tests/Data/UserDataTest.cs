using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests.Data
{
    public class UserDataTest : BaseDataTest
    {
        private readonly IUserRepository _repository;

        public UserDataTest()
        {
            _repository = new UserRepository(_context);
        }

        public async Task<User> CreateUser()
        {
            var userTest = new User()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "12345678"
            };
            var result = await _repository.CreateAsync(userTest);

            Assert.NotNull(result);
            Assert.False(result.Id == Guid.Empty);
            Assert.Equal(userTest.Name, result.Name);
            Assert.Equal(userTest.Email, result.Email);
            Assert.Equal(userTest.Password, result.Password);

            return result;
        }

        [Fact(DisplayName = "Create User")]
        [Trait("Crud", "ShouldCreateUser")]
        public async void ShouldCreateUser()
        {
            await CreateUser();
        }

        [Fact(DisplayName = "List Users")]
        [Trait("Crud", "ShouldListUser")]
        public async void ShouldListUser()
        {
            await CreateUser();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List User by Id")]
        [Trait("Crud", "ShouldListUserById")]
        public async void ShouldListUserById()
        {
            var userTest = await CreateUser();
            var result = _repository.FindByIdAsync(userTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userTest.Id, result.Id);
            Assert.Equal(userTest.Name, result.Name);
            Assert.Equal(userTest.Email, result.Email);
        }

        [Fact(DisplayName = "Update User")]
        [Trait("Crud", "ShouldUpdateUser")]
        public async void ShouldUpdateUser()
        {
            var userTest = await CreateUser();
            userTest.Name = Faker.Name.FullName();
            userTest.Email = Faker.Internet.Email();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete User")]
        [Trait("Crud", "ShouldDeleteUser")]
        public async void ShouldDeleteUser()
        {
            var userTest = await CreateUser();
            var result = await _repository.DeleteAsync(userTest.Id);

            Assert.True(result);
        }
    }
}
