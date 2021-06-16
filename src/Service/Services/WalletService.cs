using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Enums;
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
            if (result == null)
                return null;

            var wallets = _mapper.Map<IEnumerable<WalletResultDto>>(result);
            return wallets;
        }

        public async Task<IEnumerable<Guid>> FindAsyncWalletsUserIds(Guid userId)
        {
            var result = await _repository.FindAsyncWalletsUser(userId);
            return result.Select(w => w.Id);
        }

        /// <summary>
        /// Return wallets values and sum them
        /// </summary>
        /// <returns></returns>
        public async Task<WalletTotalValuesDto> WalletsTotalValues(Guid userId)
        {
            var walletsValues = await _repository.FindAsyncWalletsValues(userId);
            var walletTotalValuesDto = new WalletTotalValuesDto();
            walletTotalValuesDto.WalletsValues = walletsValues.ToList();

            walletsValues.ToList().ForEach(w =>
            {
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

            var entity = _mapper.Map<Wallet>(entityCreateDto);
            entity.UserId = userId;

            await _repository.CreateAsync(entity);
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
                return _mapper.Map<WalletResultDto>(entity);

            return null;
        }

        public async Task<int> UpdateWalletValue(Guid id, int type, double value)
        {
            var wallet = await _repository.FindByIdAsync(id);
            if (wallet == null)
                return 0;

            switch (type)
            {
                case (int) EntranceType.Income:
                    wallet.CurrentValue = wallet.CurrentValue + value;
                    break;
                case (int) EntranceType.Expanse:
                    if (value > wallet.CurrentValue)
                        throw new Exception("Insuficient founds :(");
                    wallet.CurrentValue = wallet.CurrentValue - value;
                    break;
                default:
                    wallet.CurrentValue = wallet.CurrentValue;
                    break;
            }
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid Id) => await _repository.DeleteAsync(Id);
    }
}
