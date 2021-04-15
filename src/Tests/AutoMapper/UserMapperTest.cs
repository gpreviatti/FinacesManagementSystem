using System;
using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.User;
using Domain.Dtos.Wallet;
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
                Id = Guid.NewGuid(),
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
            var entity = new User()
            {
                Id = Guid.NewGuid(),
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName(),
                Password = EncryptHelper.HashField("mudar@123"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Wallets = new List<Wallet>(),
                Categories = new List<Category>()
            };

            var walletResultDto = _mapper.Map<IEnumerable<WalletResultDto>>(entity.Wallets);
            var categoryResultDto = _mapper.Map<IEnumerable<CategoryResultDto>>(entity.Categories);

            var userResultDto = _mapper.Map<UserResultDto>(entity);
            Assert.NotNull(userResultDto);
            Assert.Equal(entity.Id, userResultDto.Id);
            Assert.Equal(entity.Email, userResultDto.Email);
            Assert.Equal(entity.Name, userResultDto.Name);
            Assert.Equal(entity.Email, userResultDto.Email);
            Assert.Equal(entity.Name, userResultDto.Name);
            Assert.Equal(userResultDto.Wallets, walletResultDto);
            Assert.Equal(userResultDto.Categories, categoryResultDto);
        }
    }
}
