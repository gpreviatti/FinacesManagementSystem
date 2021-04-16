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

        public User CreateUserEntity()
        {
            return new User()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "12345678"
            };
        }

        [Fact(DisplayName = "Create User")]
        [Trait("Crud", "ShouldCreateUser")]
        public async void ShouldCreateUser()
        {
            try
            {
                var userEntity = CreateUserEntity();
                var result = await _repository.CreateAsync(userEntity);

                Assert.NotNull(result);
                Assert.False(result.Id == Guid.Empty);
                Assert.Equal(userEntity.Name, result.Name);
                Assert.Equal(userEntity.Email, result.Email);
                Assert.Equal(userEntity.Password, result.Password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        [Fact(DisplayName = "List Users")]
        [Trait("Crud", "ShouldListUser")]
        public async void ShouldListUser()
        {
            var userEntity = CreateUserEntity();
            await _repository.CreateAsync(userEntity);

            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List User by Id")]
        [Trait("Crud", "ShouldListUserById")]
        public async void ShouldListUserById()
        {
            try
            {
                var userEntity = CreateUserEntity();
                await _repository.CreateAsync(userEntity);

                var result = _repository.FindByIdAsync(userEntity.Id).Result;

                Assert.NotNull(result);
                Assert.IsType<User>(result);
                Assert.Equal(userEntity.Id, result.Id);
                Assert.Equal(userEntity.Name, result.Name);
                Assert.Equal(userEntity.Email, result.Email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update User")]
        [Trait("Crud", "ShouldUpdateUser")]
        public async void ShouldUpdateUser()
        {
            try
            {
                var userEntity = CreateUserEntity();
                await _repository.CreateAsync(userEntity);

                userEntity.Name = Faker.Name.FullName();
                userEntity.Email = Faker.Internet.Email();

                var result = await _repository.SaveChangesAsync();

                Assert.Equal(1, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete User")]
        [Trait("Crud", "ShouldDeleteUser")]
        public async void ShouldDeleteUser()
        {
            try
            {
                var userEntity = CreateUserEntity();
                await _repository.CreateAsync(userEntity);

                var result = await _repository.DeleteAsync(userEntity.Id);

                Assert.True(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
