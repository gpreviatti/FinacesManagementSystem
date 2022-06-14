using Domain.Dtos.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using System;
using System.Threading.Tasks;

namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResultDto> FindByIdAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);

        return result.MapperResultDto();
    }

    public UserResultDto CreateAsync(UserCreateDto userCreateDto)
    {
        var user = userCreateDto.Mapper();

        //user.Password = EncryptHelper.HashField(user.Password);

        _ = _repository.CreateAsync(user);

        return user.MapperResultDto();
    }

    public async Task<UserResultDto> UpdateAsync(UserUpdateDto userUpdateDto)
    {
        //if (userUpdateDto.Password != null)
        //    userUpdateDto.Password = EncryptHelper.HashField(userUpdateDto.Password);

        var result = await _repository.FindByIdAsync(userUpdateDto.Id);

        if (result == null)
            return null;

        var user = userUpdateDto.Mapper();

        _ = _repository.SaveChangesAsync();

        return user.MapperResultDto();
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}
