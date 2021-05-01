using System;
using System.Collections.Generic;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Helpers.Enuns;
using Xunit;

namespace Tests.AutoMapper
{
    public class EntranceMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform EntranceCreateDto to Entrance")]
        [Trait("AutoMapper", "EntranceCreateDtoToEntrance")]
        public void EntranceCreateDtoToEntrance()
        {
            var entityCreateDto = new EntranceCreateDto()
            {
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                CategoryId = Guid.NewGuid(),
                Ticker = "BIDI4",
                Type = (int) EEntranceType.expanse,
                Value = 100,
                WalletId = Guid.NewGuid()
            };

            var entity = _mapper.Map<Entrance>(entityCreateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Description, entityCreateDto.Description);
            Assert.Equal(entity.Observation, entityCreateDto.Observation);
            Assert.Equal(entity.Ticker, entityCreateDto.Ticker);
            Assert.Equal(entity.Value, entityCreateDto.Value);
        }

        [Fact(DisplayName = "Should transform EntranceUpdateDto to Entrance")]
        [Trait("AutoMapper", "EntranceCreateDtoToEntrance")]
        public void EntranceUpdateDtoToEntrance()
        {
            var entityUpdateDto = new EntranceUpdateDto()
            {
                Id = Guid.NewGuid(),
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                CategoryId = Guid.NewGuid(),
                Ticker = "BIDI4",
                Type = (int)EEntranceType.expanse,
                Value = 100,
                WalletId = Guid.NewGuid()

            };

            var entity = _mapper.Map<Entrance>(entityUpdateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, entityUpdateDto.Id);
            Assert.Equal(entity.Description, entityUpdateDto.Description);
            Assert.Equal(entity.Observation, entityUpdateDto.Observation);
            Assert.Equal(entity.Ticker, entityUpdateDto.Ticker);
            Assert.Equal(entity.Value, entityUpdateDto.Value);
        }

        [Fact(DisplayName = "Should transform Entrance to EntranceResultDto")]
        [Trait("AutoMapper", "EntranceCreateDtoToEntrance")]
        public void EntranceToEntranceResultDto()
        {
            var entity = new Entrance()
            {
                Id = Guid.NewGuid(),
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                Ticker = "BIDI4",
                Type = (int)EEntranceType.expanse,
                Value = 100,
                Wallet = new Wallet(),
                Category = new Category(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var entityResultDto = _mapper.Map<EntranceResultDto>(entity);
            Assert.NotNull(entityResultDto);
            Assert.Equal(entity.Id, entityResultDto.Id);
            Assert.Equal(entity.Description, entityResultDto.Description);
            Assert.Equal(entity.Observation, entityResultDto.Observation);
            Assert.Equal(entity.Ticker, entityResultDto.Ticker);
            Assert.Equal(entity.Value, entityResultDto.Value);
            Assert.Equal(entity.CreatedAt, entityResultDto.CreatedAt);
            Assert.Equal(entity.UpdatedAt, entityResultDto.UpdatedAt);
        }
    }
}
