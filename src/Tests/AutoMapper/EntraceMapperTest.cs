using System;
using System.Collections.Generic;
using Domain.Dtos.Entrace;
using Domain.Entities;
using Helpers.Enuns;
using Xunit;

namespace Tests.AutoMapper
{
    public class EntraceMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform EntraceCreateDto to Entrace")]
        [Trait("AutoMapper", "EntraceCreateDtoToEntrace")]
        public void EntraceCreateDtoToEntrace()
        {
            var entityCreateDto = new EntraceCreateDto()
            {
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                CategoryId = Guid.NewGuid(),
                Ticker = "BIDI4",
                Type = (int) EEntraceType.expanse,
                Value = 100,
                WalletId = Guid.NewGuid()
            };

            var entity = _mapper.Map<Entrace>(entityCreateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Description, entityCreateDto.Description);
            Assert.Equal(entity.Observation, entityCreateDto.Observation);
            Assert.Equal(entity.CategoryId, entityCreateDto.CategoryId);
            Assert.Equal(entity.Ticker, entityCreateDto.Ticker);
            Assert.Equal(entity.Value, entityCreateDto.Value);
            Assert.Equal(entity.WalletId, entityCreateDto.WalletId);
        }

        [Fact(DisplayName = "Should transform EntraceUpdateDto to Entrace")]
        [Trait("AutoMapper", "EntraceCreateDtoToEntrace")]
        public void EntraceUpdateDtoToEntrace()
        {
            var entityUpdateDto = new EntraceUpdateDto()
            {
                Id = new Guid(),
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                CategoryId = Guid.NewGuid(),
                Ticker = "BIDI4",
                Type = (int)EEntraceType.expanse,
                Value = 100,
                WalletId = Guid.NewGuid()

            };

            var entity = _mapper.Map<Entrace>(entityUpdateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, entityUpdateDto.Id);
            Assert.Equal(entity.Description, entityUpdateDto.Description);
            Assert.Equal(entity.Observation, entityUpdateDto.Observation);
            Assert.Equal(entity.CategoryId, entityUpdateDto.CategoryId);
            Assert.Equal(entity.Ticker, entityUpdateDto.Ticker);
            Assert.Equal(entity.Value, entityUpdateDto.Value);
            Assert.Equal(entity.WalletId, entityUpdateDto.WalletId);
        }

        [Fact(DisplayName = "Should transform Entrace to EntraceResultDto")]
        [Trait("AutoMapper", "EntraceCreateDtoToEntrace")]
        public void EntraceToEntraceResultDto()
        {
            var entity = new Entrace()
            {
                Id = new Guid(),
                Description = Faker.Lorem.Sentence(100),
                Observation = Faker.Lorem.Sentence(100),
                CategoryId = Guid.NewGuid(),
                Ticker = "BIDI4",
                Type = (int)EEntraceType.expanse,
                Value = 100,
                WalletId = Guid.NewGuid()
            };

            var entityResultDto = _mapper.Map<EntraceResultDto>(entity);
            Assert.NotNull(entityResultDto);
            Assert.Equal(entity.Id, entityResultDto.Id);
            Assert.Equal(entity.Description, entityResultDto.Description);
            Assert.Equal(entity.Observation, entityResultDto.Observation);
            Assert.Equal(entity.CategoryId, entityResultDto.CategoryId);
            Assert.Equal(entity.Ticker, entityResultDto.Ticker);
            Assert.Equal(entity.Value, entityResultDto.Value);
            Assert.Equal(entity.WalletId, entityResultDto.WalletId);
        }
    }
}
