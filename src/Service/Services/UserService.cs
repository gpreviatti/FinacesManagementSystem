using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Helpers;

namespace Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResultDto> FindByIdAsync(Guid id)
        {
            var result = await _repository.FindByIdAsync(id);
            return _mapper.Map<UserResultDto>(result);
        }

        public async Task<IEnumerable<UserResultDto>> FindAllAsync()
        {
            var result = await _repository.FindAllAsync();
            return _mapper.Map<IEnumerable<UserResultDto>>(result);
        }

        public async Task<UserResultDto> CreateAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = EncryptHelper.HashField(user.Password);

            var result = await _repository.CreateAsync(user);
            return _mapper.Map<UserResultDto>(user);
        }

        public async Task<UserResultDto> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto.Password != null)
                userUpdateDto.Password = EncryptHelper.HashField(userUpdateDto.Password);

            var result = await _repository.FindByIdAsync(userUpdateDto.Id);

            if (result == null)
                return null;

            var user = _mapper.Map(userUpdateDto, result);

            var savedChanges = await _repository.SaveChangesAsync();

            if (savedChanges > 0)
                return _mapper.Map<UserResultDto>(user);

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
