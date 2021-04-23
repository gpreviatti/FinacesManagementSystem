using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Entrace;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Helpers.Enuns;

namespace Service.Services
{
    public class EntraceService : BaseService, IEntraceService
    {
        private readonly IEntraceRepository _repository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EntraceService(
            IMapper mapper,
            IEntraceRepository repository,
            IWalletRepository walletRepository,
            ICategoryRepository categoryRepository
        ) 
        {
            _mapper = mapper;
            _repository = repository;
            _walletRepository = walletRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EntraceResultDto> FindByIdAsync(Guid Id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Id);
                return _mapper.Map<EntraceResultDto>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<IEnumerable<EntraceResultDto>> FindAllAsync()
        {
            try
            {
                var result = await _repository.FindAllAsync();
                return _mapper.Map<IEnumerable<EntraceResultDto>>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntraceResultDto> CreateAsync(EntraceCreateDto entraceCreateDto)
        {
            try
            {
                var wallet = _walletRepository.FindByIdAsync(entraceCreateDto.WalletId).Result;
                if (wallet == null)
                {
                    return null;
                }

                switch (entraceCreateDto.Type)
                {
                    case (int) EEntraceType.income:
                        wallet.CurrentValue = wallet.CurrentValue + entraceCreateDto.Value;
                        break;
                    case (int) EEntraceType.expanse:
                        wallet.CurrentValue = wallet.CurrentValue - entraceCreateDto.Value;
                        break;
                    default:
                        wallet.CurrentValue = wallet.CurrentValue;
                        break;
                }
                if (_walletRepository.SaveChangesAsync().Result.Equals(0))
                {
                    return null;
                }

                var category = _categoryRepository.FindByIdAsync(entraceCreateDto.CategoryId).Result;
                if (category == null)
                {
                    return null;
                }

                var entrace = _mapper.Map<Entrace>(entraceCreateDto);
                entrace.Wallet = wallet;
                entrace.Category = category;

                var result = await _repository.CreateAsync(entrace);
                return _mapper.Map<EntraceResultDto>(entrace);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntraceResultDto> UpdateAsync(EntraceResultDto entraceResultDto)
        {
            try
            {
                var result = await _repository.FindByIdAsync(entraceResultDto.Id);

                if (result == null)
                {
                    return null;
                }

                var wallet = _walletRepository.FindByIdAsync(entraceResultDto.WalletId).Result;
                if (wallet == null)
                {
                    return null;
                }

                switch (entraceResultDto.Type)
                {
                    case (int)EEntraceType.income:
                        wallet.CurrentValue = wallet.CurrentValue + entraceResultDto.Value;
                        break;
                    case (int)EEntraceType.expanse:
                        wallet.CurrentValue = wallet.CurrentValue - entraceResultDto.Value;
                        break;
                    default:
                        wallet.CurrentValue = wallet.CurrentValue;
                        break;
                }
                if (_walletRepository.SaveChangesAsync().Result.Equals(0))
                {
                    return null;
                }

                if (_categoryRepository.FindByIdAsync(entraceResultDto.CategoryId).Result == null)
                {
                    return null;
                }

                var entrace = _mapper.Map(entraceResultDto, result);

                var savedChanges = await _repository.SaveChangesAsync();

                if (savedChanges > 0)
                {
                    return entraceResultDto;
                }
                return null;
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                return await _repository.DeleteAsync(Id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}
