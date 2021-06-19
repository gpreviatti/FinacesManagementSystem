using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.WalletType;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Service.Services
{
    public class WalletTypeService : BaseService, IWalletTypeService
    {
        private readonly IWalletTypeRepository _repository;

        public WalletTypeService(IWalletTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WalletTypeResultDto> FindByIdAsync(Guid id)
        {
            var result = await _repository.FindByIdAsync(id);
            return _mapper.Map<WalletTypeResultDto>(result);
        }

        public async Task<IEnumerable<WalletTypeResultDto>> FindAllAsync()
        {
            var result = await _repository.FindAllAsync();
            return _mapper.Map<IEnumerable<WalletTypeResultDto>>(result);
        }

        public async Task<WalletTypeResultDto> CreateAsync(WalletTypeCreateDto entityCreateDto)
        {
            var entity = _mapper.Map<WalletType>(entityCreateDto);

            await _repository.CreateAsync(entity);
            return _mapper.Map<WalletTypeResultDto>(entity);
        }

        public async Task<WalletTypeResultDto> UpdateAsync(WalletTypeUpdateDto entityUpdateDto)
        {
            
            var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

            if (result == null)
                return null;

            var entity = _mapper.Map(entityUpdateDto, result);

            var savedChanges = await _repository.SaveChangesAsync();

            if (savedChanges > 0)
                return _mapper.Map<WalletTypeResultDto>(entity);

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
