using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Service.Services
{
    public class WalletService : BaseService, IWalletService
    {
        private readonly IWalletRepository _repository;

        public WalletService(IWalletRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region Find
        public async Task<WalletResultDto> FindByIdAsync(Guid Id)
        {
            var result = await _repository.FindByIdAsync(Id);
            return _mapper.Map<WalletResultDto>(result);
        }

        public async Task<WalletUpdateDto> FindByIdUpdateAsync(Guid id)
        {
            var result = await _repository.FindByIdAsync(id);
            return _mapper.Map<WalletUpdateDto>(result);
        }

        public async Task<IEnumerable<WalletResultDto>> FindAsyncWalletsUser(Guid userId)
        {
            var result = await _repository.FindAsyncWalletsUser(userId);
            var wallets = _mapper.Map<IEnumerable<WalletResultDto>>(result);
            wallets
                .ToList()
                .ForEach(w => w.CurrentValue = w.Entrances.Select(w => w.Value).Sum());
            return wallets;
        }

        public List<Guid> FindAsyncWalletsUserIds(Guid userId) => 
            _repository.FindAsyncWalletsUser(userId).Result.Select(w => w.Id).ToList();

        /// <summary>
        /// Return wallets values and sum them
        /// </summary>
        /// <returns></returns>
        public async Task<WalletTotalValuesDto> WalletsTotalValues(Guid userId) 
        {
            var walletsValues = await _repository.FindAsyncWalletsValues(userId);
            var walletTotalValuesDto = new WalletTotalValuesDto();
            walletTotalValuesDto.WalletsValues = walletsValues.ToList();

            walletsValues.ToList().ForEach(w => {
                walletTotalValuesDto.TotalIncomes += w.TotalIncomes;
                walletTotalValuesDto.TotalExpanses += w.TotalExpanses;
            });

            return walletTotalValuesDto;
        }
        #endregion

        public async Task<WalletResultDto> CreateAsync(WalletCreateDto entityCreateDto, Guid userId)
        { 
            if (entityCreateDto.WalletTypeId == Guid.Empty)
                return null;

            if (userId == Guid.Empty)
                return null;

            entityCreateDto.UserId = userId;
            var entity = _mapper.Map<Wallet>(entityCreateDto);

            var result = await _repository.CreateAsync(entity);
            return _mapper.Map<WalletResultDto>(entity);
        }

        public async Task<WalletResultDto> UpdateAsync(WalletUpdateDto entityUpdateDto)
        {
            var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

            if (result == null)
                return null;

            var entity = _mapper.Map(entityUpdateDto, result);

            var savedChanges = await _repository.SaveChangesAsync();

            if (savedChanges > 0)
            {
                return _mapper.Map<WalletResultDto>(entity);
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid Id) => await _repository.DeleteAsync(Id);
    }
}
