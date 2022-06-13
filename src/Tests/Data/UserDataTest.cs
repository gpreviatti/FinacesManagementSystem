using System;
using System.Diagnostics;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
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

        public static User CreateUserEntity()
        {
            return new ()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "12345678"
            };
        }

        [Fact(DisplayName = "Create User")]
        [Trait("Data", "User")]
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
                Assert.True(false);
                Debug.WriteLine(e);
            }

        }

        [Fact(DisplayName = "List Users")]
        [Trait("Data", "User")]
        public async void ShouldListUser()
        {
            try
            {
                var userEntity = CreateUserEntity();
                await _repository.CreateAsync(userEntity);

                var result = _repository.FindAllAsync().Result;

                Assert.NotNull(result);
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List User by Id")]
        [Trait("Data", "User")]
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
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Find User By Login")]
        [Trait("Data", "User")]
        public async void ShouldFindUserByLogin()
        {
            try
            {
                var userEntity = CreateUserEntity();

                await _repository.CreateAsync(userEntity);

                var result = await _repository.FindByLogin(userEntity.Email);

                Assert.True(result != null);
                Assert.Equal(userEntity.Email, result.Email);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Not Find User By Login")]
        [Trait("Data", "User")]
        public async void ShouldNotFindUserByLogin()
        {
            try
            {
                var userEntity = CreateUserEntity();

                await _repository.CreateAsync(userEntity);

                var result = await _repository.FindByLogin(Faker.Internet.Email());

                Assert.True(result == null);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update User")]
        [Trait("Data", "User")]
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
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete User")]
        [Trait("Data", "User")]
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
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }
    }
}
