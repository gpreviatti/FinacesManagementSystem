using System;
using Domain.Dtos.WalletType;
using Domain.Entities;
using Xunit;

namespace Tests.AutoMapper
{
    public class WalletTypeTypeMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform WalletTypeCreateDto to WalletType")]
        [Trait("AutoMapper", "WalletTypeCreateDtoToWalletType")]
        public void WalletTypeCreateDtoToWalletType()
        {
            var entityCreateDto = new WalletTypeCreateDto()
            {
                Name = Faker.Name.First()
            };

            var entity = _mapper.Map<WalletType>(entityCreateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Name, entityCreateDto.Name);
        }

        [Fact(DisplayName = "Should transform WalletTypeUpdateDto to WalletType")]
        [Trait("AutoMapper", "WalletTypeCreateDtoToWalletType")]
        public void WalletTypeUpdateDtoToWalletType()
        {
            var entityUpdateDto = new WalletTypeUpdateDto()
            {
                Id = new Guid(),
                Name = Faker.Name.First()
            };

            var entity = _mapper.Map<WalletType>(entityUpdateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, entityUpdateDto.Id);
            Assert.Equal(entity.Name, entityUpdateDto.Name);
        }

        [Fact(DisplayName = "Should transform WalletType to WalletTypeResultDto")]
        [Trait("AutoMapper", "WalletTypeCreateDtoToWalletType")]
        public void WalletTypeToWalletTypeResultDto()
        {
            var entity = new WalletType()
            {
                Id = new Guid(),
                Name = Faker.Name.First(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var entityResultDto = _mapper.Map<WalletTypeResultDto>(entity);
            Assert.NotNull(entityResultDto);
            Assert.Equal(entity.Id, entityResultDto.Id);
            Assert.Equal(entity.Name, entityResultDto.Name);
            Assert.Equal(entity.CreatedAt, entityResultDto.CreatedAt);
            Assert.Equal(entity.UpdatedAt, entityResultDto.UpdatedAt);
        }
    }
}
