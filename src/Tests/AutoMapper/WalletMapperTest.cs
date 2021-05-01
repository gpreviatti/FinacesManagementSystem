using System;
using System.Collections.Generic;
using Domain.Dtos.Entrance;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;
using Domain.Entities;
using Helpers;
using Xunit;

namespace Tests.AutoMapper
{
    public class WalletMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform WalletCreateDto to Wallet")]
        [Trait("AutoMapper", "WalletCreateDtoToWallet")]
        public void WalletCreateDtoToWallet()
        {
            var entityCreateDto = new WalletCreateDto()
            {
                Name = Faker.Name.First(),
                Description = Faker.Lorem.Sentence(100),
                DueDate = DateTime.Now,
                CloseDate = DateTime.Now.AddDays(15),
                CurrentValue = 100,
                WalletTypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                
            };

            var entity = _mapper.Map<Wallet>(entityCreateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Name, entityCreateDto.Name);
            Assert.Equal(entity.Description, entityCreateDto.Description);
            Assert.Equal(entity.DueDate, entityCreateDto.DueDate);
            Assert.Equal(entity.CloseDate, entityCreateDto.CloseDate);
            Assert.Equal(entity.CurrentValue, entityCreateDto.CurrentValue);
        }

        [Fact(DisplayName = "Should transform WalletUpdateDto to Wallet")]
        [Trait("AutoMapper", "WalletCreateDtoToWallet")]
        public void WalletUpdateDtoToWallet()
        {
            var entityUpdateDto = new WalletUpdateDto()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First(),
                Description = Faker.Lorem.Sentence(100),
                DueDate = DateTime.Now,
                CloseDate = DateTime.Now.AddDays(15),
                CurrentValue = 100,
                WalletTypeId = Guid.NewGuid()
            };

            var entity = _mapper.Map<Wallet>(entityUpdateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, entityUpdateDto.Id);
            Assert.Equal(entity.Description, entityUpdateDto.Description);
            Assert.Equal(entity.DueDate, entityUpdateDto.DueDate);
            Assert.Equal(entity.CloseDate, entityUpdateDto.CloseDate);
            Assert.Equal(entity.CurrentValue, entityUpdateDto.CurrentValue);
        }

        [Fact(DisplayName = "Should transform Wallet to WalletResultDto")]
        [Trait("AutoMapper", "WalletToWalletResultDto")]
        public void WalletToWalletResultDto()
        {
            var entity = new Wallet()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First(),
                Description = Faker.Lorem.Sentence(100),
                DueDate = DateTime.Now,
                CloseDate = DateTime.Now.AddDays(15),
                CurrentValue = 100,
                WalletType = new WalletType(),
                Entrances = new List<Entrance>()
            };

            var entityWalletTypeMapper = _mapper.Map<WalletTypeResultDto>(entity.WalletType);
            var entityEntranceMapper = _mapper.Map<IEnumerable<EntranceResultDto>>(entity.Entrances);

            var entityResultDto = _mapper.Map<WalletResultDto>(entity);
            Assert.NotNull(entityResultDto);
            Assert.Equal(entity.Id, entityResultDto.Id);
            Assert.Equal(entity.Description, entityResultDto.Description);
            Assert.Equal(entity.DueDate, entityResultDto.DueDate);
            Assert.Equal(entity.CloseDate, entityResultDto.CloseDate);
            Assert.Equal(entity.CurrentValue, entityResultDto.CurrentValue);
            Assert.Equal(entityResultDto.Entrances, entityEntranceMapper);
            //Assert.Equal(entityResultDto.WalletType, entityWalletTypeMapper);
        }
    }
}
