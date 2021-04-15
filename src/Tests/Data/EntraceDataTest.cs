using System;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class EntraceDataTest : BaseDataTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletTypeRepository _walletTypeRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEntraceRepository _repository;

        public EntraceDataTest()
        {
            _userRepository = new UserRepository(_context);
            _walletTypeRepository = new WalletTypeRepository(_context);
            _walletRepository = new WalletRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _repository = new EntraceRepository(_context);

        }

        private async Task<Entrace> CreateEntrace()
        {
            var entityTest = new Entrace()
            {
                Id = Guid.NewGuid(),
                Description = Faker.Lorem.Sentence(200),
                Observation = Faker.Lorem.Sentence(200),
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Category = new Category() { Name = "Test Category" },
                Wallet = new Wallet() { Name = "Test Wallet" },
            };
            return await _repository.CreateAsync(entityTest);
        }

        [Fact(DisplayName = "Create Entrace")]
        [Trait("Crud", "ShouldCreateEntrace")]
        public async void ShouldCreateEntrace()
        {
            var userTest = _userRepository.CreateAsync(new User() { 
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "mudar@1234"
            }).Result;
            Assert.NotNull(userTest);

            var walletTypeTest = _walletTypeRepository.CreateAsync(new WalletType()
            {
                Name = "Test WalletType"
            }).Result;
            Assert.NotNull(walletTypeTest);

            var walletTest = _walletRepository.CreateAsync(new Wallet() {
                Name = Faker.Name.FullName(),
                CloseDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                Description = Faker.Lorem.Sentence(100),
                CurrentValue = 1000,
                WalletType = walletTypeTest,
                User = userTest
            }).Result;
            Assert.NotNull(walletTest);

            var categoryTest  = _categoryRepository.CreateAsync(new Category()
            {
                Name = Faker.Name.FullName()
            }).Result;
            Assert.NotNull(categoryTest);

            var entityTest = new Entrace()
            {
                Description = Faker.Lorem.Sentence(200),
                Observation = Faker.Lorem.Sentence(200),
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                Category = categoryTest,
                Wallet = walletTest,
            };

            var result = await _repository.CreateAsync(entityTest);

            Assert.NotNull(result);
            Assert.False(result.Id == Guid.Empty);
        }

        [Fact(DisplayName = "List Entraces")]
        [Trait("Crud", "ShouldListEntrace")]
        public async void ShouldListEntrace()
        {
            await CreateEntrace();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List Entrace by Id")]
        [Trait("Crud", "ShouldListEntraceById")]
        public async void ShouldListEntraceById()
        {
            var entityTest = await CreateEntrace();
            var result = _repository.FindByIdAsync(entityTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<Entrace>(result);
            Assert.Equal(entityTest.Id, result.Id);
        }

        [Fact(DisplayName = "Update Entrace")]
        [Trait("Crud", "ShouldUpdateEntrace")]
        public async void ShouldUpdateEntrace()
        {
            var entityTest = await CreateEntrace();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete Entrace")]
        [Trait("Crud", "ShouldDeleteEntrace")]
        public async void ShouldDeleteEntrace()
        {
            var entityTest = await CreateEntrace();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
