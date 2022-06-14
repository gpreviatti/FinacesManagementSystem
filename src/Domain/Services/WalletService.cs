using Domain.Dtos.Wallet;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services;
public class WalletService : IWalletService
{
    private readonly IWalletRepository _repository;

    public WalletService(IWalletRepository repository)
    {
        _repository = repository;
    }

    #region Find
    public async Task<WalletResultDto> FindByIdAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);

        return result.MapperResultDto();
    }

    public async Task<WalletUpdateDto> FindByIdUpdateAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);

        return result.MapperUpdateDto();
    }

    public async Task<IEnumerable<WalletResultDto>> FindAsyncWalletsUser(Guid userId)
    {
        var result = await _repository.FindAsyncWalletsUser(userId);

        return result.MapperResultDto();
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

        walletsValues.ToList().ForEach(w =>
        {
            walletTotalValuesDto.TotalIncomes += w.TotalIncomes;
            walletTotalValuesDto.TotalExpanses += w.TotalExpanses;
        });

        walletTotalValuesDto.WalletsValues = walletsValues;

        return walletTotalValuesDto;
    }
    #endregion

    public async Task<WalletResultDto> CreateAsync(WalletCreateDto walletCreateDto, Guid userId)
    {
        if (walletCreateDto.WalletTypeId == Guid.Empty)
            return null;

        if (userId == Guid.Empty)
            return null;

        var entity = walletCreateDto.Mapper();

        entity.UserId = userId;

        _ = await _repository.CreateAsync(entity);

        return entity.MapperResultDto();
    }

    public async Task<WalletResultDto> UpdateAsync(WalletUpdateDto walletUpdateDto)
    {
        var result = await _repository.FindByIdAsync(walletUpdateDto.Id);

        if (result == null)
            return null;

        var entity = walletUpdateDto.Mapper();

        _ = _repository.SaveChangesAsync();

        return entity.MapperResultDto();
    }

    public async Task<WalletResultDto> UpdateWalletValue(Guid id, int type, double value)
    {
        var wallet = await _repository.FindByIdAsync(id);
        if (wallet == null)
            return null;

        switch (type)
        {
            case (int)EntranceType.Income:
                wallet.CurrentValue += value;
                break;
            case (int)EntranceType.Expanse:
                if (value > wallet.CurrentValue)
                    throw new Exception("Insuficient founds :(");

                wallet.CurrentValue -= value;
                break;
            default:
                wallet.CurrentValue = wallet.CurrentValue;
                break;
        }

        _ = _repository.SaveChangesAsync();

        return wallet.MapperResultDto();
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}
