using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Entrace;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Service.Services
{
    public class EntraceService : BaseService, IEntraceService
    {
        private readonly IEntraceRepository _repository;

        public EntraceService(IEntraceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                System.Console.WriteLine(exception);
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
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntraceResultDto> CreateAsync(EntraceCreateDto entraceCreateDto)
        {
            try
            {
                var entrace = _mapper.Map<Entrace>(entraceCreateDto);

                var result = await _repository.CreateAsync(entrace);
                return _mapper.Map<EntraceResultDto>(entrace);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntraceResultDto> UpdateAsync(EntraceUpdateDto entraceUpdateDto)
        {
            try
            {
                var result = await _repository.FindByIdAsync(entraceUpdateDto.Id);

                if (result == null)
                {
                    return null;
                }

                var entrace = _mapper.Map(entraceUpdateDto, result);

                var savedChanges = await _repository.SaveChangesAsync();

                if (savedChanges > 0)
                {
                    return _mapper.Map<EntraceResultDto>(entrace);
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
