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

        public async Task<WalletTypeResultDto> FindByIdAsync(Guid Id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Id);
                return _mapper.Map<WalletTypeResultDto>(result);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<IEnumerable<WalletTypeResultDto>> FindAllAsync()
        {
            try
            {
                var result = await _repository.FindAllAsync();
                return _mapper.Map<IEnumerable<WalletTypeResultDto>>(result);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<WalletTypeResultDto> CreateAsync(WalletTypeCreateDto entityCreateDto)
        {
            try
            {
                var entity = _mapper.Map<WalletType>(entityCreateDto);

                var result = await _repository.CreateAsync(entity);
                return _mapper.Map<WalletTypeResultDto>(entity);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<WalletTypeResultDto> UpdateAsync(WalletTypeUpdateDto entityUpdateDto)
        {
            try
            {
                var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

                if (result == null)
                {
                    return null;
                }

                var entity = _mapper.Map(entityUpdateDto, result);

                var savedChanges = await _repository.SaveChangesAsync();

                if (savedChanges > 0)
                {
                    return _mapper.Map<WalletTypeResultDto>(entity);
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
                System.Console.WriteLine(exception);
                return false;
            }
        }
    }
}
