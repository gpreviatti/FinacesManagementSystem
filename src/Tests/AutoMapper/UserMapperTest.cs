using System;
using Domain.Dtos.User;
using Domain.Entities;
using Helpers;
using Xunit;

namespace Tests.AutoMapper
{
    public class UserMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform UserCreateDto to User")]
        [Trait("AutoMapper", "UserCreateDtoToUser")]
        public void UserCreateDtoToUser()
        {
            var userCreateDto = new UserCreateDto()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = EncryptHelper.HashField("mudar@123")
            };

            var user = _mapper.Map<User>(userCreateDto);
            Assert.NotNull(user);
            Assert.Equal(user.Email, userCreateDto.Email);
            Assert.Equal(user.Name, userCreateDto.Name);
            Assert.Equal(user.Password, userCreateDto.Password);
        }

        [Fact(DisplayName = "Should transform UserUpdateDto to User")]
        [Trait("AutoMapper", "UserCreateDtoToUser")]
        public void UserUpdateDtoToUser()
        {
            var userUpdateDto = new UserUpdateDto()
            {
                Id = new Guid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = EncryptHelper.HashField("mudar@123")
            };

            var user = _mapper.Map<User>(userUpdateDto);
            Assert.NotNull(user);
            Assert.Equal(user.Id, userUpdateDto.Id);
            Assert.Equal(user.Email, userUpdateDto.Email);
            Assert.Equal(user.Name, userUpdateDto.Name);
            Assert.Equal(user.Password, userUpdateDto.Password);
        }

        [Fact(DisplayName = "Should transform User to UserResultDto")]
        [Trait("AutoMapper", "UserCreateDtoToUser")]
        public void UserToUserResultDto()
        {
            var user = new User()
            {
                Id = new Guid(),
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName(),
                Password = EncryptHelper.HashField("mudar@123"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var userResultDto = _mapper.Map<UserResultDto>(user);
            Assert.NotNull(userResultDto);
            Assert.Equal(user.Id, userResultDto.Id);
            Assert.Equal(user.Email, userResultDto.Email);
            Assert.Equal(user.Name, userResultDto.Name);
        }
    }
}
