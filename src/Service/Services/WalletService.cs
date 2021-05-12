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
        private readonly IEntranceService _entranceService;

        public WalletService(IWalletRepository repository, IEntranceService entranceService, IMapper mapper)
        {
            _repository = repository;
            _entranceService = entranceService;
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

        public async Task<IEnumerable<WalletResultDto>> FindAsyncWalletsUser()
        {
            var result = await _repository.FindAsyncWalletsUser(UserId);
            var wallets = _mapper.Map<IEnumerable<WalletResultDto>>(result);
            wallets.ToList().ForEach(w => w.CurrentValue = _entranceService.TotalEntrancesByWallet(w.Id).Result);
            return wallets;
        }

        /// <summary>
        /// Return wallets values and sum them
        /// </summary>
        /// <returns></returns>
        public async Task<WalletTotalValuesAndEntrancesDto> WalletsTotalValuesAndLastTenEntrances() 
        {
            var walletsValues = await _repository.FindAsyncWalletsValues(UserId);
            var walletTotalValuesDto = new WalletTotalValuesAndEntrancesDto();
            walletTotalValuesDto.WalletsValues = walletsValues.ToList();
            walletTotalValuesDto.Entrances = _entranceService.FindAsyncLastFiveEntrancesWithCategories().Result;

            walletsValues.ToList().ForEach(w => {
                walletTotalValuesDto.TotalIncomes += w.TotalIncomes;
                walletTotalValuesDto.TotalExpanses += w.TotalExpanses;
            });

            return walletTotalValuesDto;
        }
        #endregion

        public async Task<WalletResultDto> CreateAsync(WalletCreateDto entityCreateDto)
        { 
            if (entityCreateDto.WalletTypeId == Guid.Empty)
                return null;

            entityCreateDto.UserId = UserId;
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
