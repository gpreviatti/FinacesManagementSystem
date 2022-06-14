using Domain.Dtos.WalletType;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services;

public class WalletTypeService : IWalletTypeService
{
    private readonly IWalletTypeRepository _repository;

    public WalletTypeService(IWalletTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<WalletTypeResultDto> FindByIdAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);

        return result.MapperResultDto();
    }

    public async Task<IEnumerable<WalletTypeResultDto>> FindAllAsync()
    {
        var result = await _repository.FindAllAsync();

        return result.MapperResultDto();
    }

    public async Task<WalletTypeResultDto> CreateAsync(WalletTypeCreateDto entityCreateDto)
    {
        var entity = entityCreateDto.Mapper();

        _ = await _repository.CreateAsync(entity);

        return entity.MapperResultDto();
    }

    public async Task<WalletTypeResultDto> UpdateAsync(WalletTypeUpdateDto entityUpdateDto)
    {
        var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

        if (result == null)
            return null;

        var entity = entityUpdateDto.Mapper();

        _ = await _repository.SaveChangesAsync();

        return entity.MapperResultDto();
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}
