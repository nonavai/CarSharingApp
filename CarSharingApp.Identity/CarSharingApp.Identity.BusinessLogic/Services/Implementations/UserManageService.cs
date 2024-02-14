using AutoMapper;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.DataAccess.Specifications;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Exceptions;

namespace CarSharingApp.Identity.BusinessLogic.Services.Implementations;

public class UserManageService : IUserManageService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IMapper _mapper;

    public UserManageService(IUserRepository userRepository, IMapper mapper, IUserInfoRepository userInfoRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userInfoRepository = userInfoRepository;
    }

    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
        
        var userDto = _mapper.Map<UserDto>(user);
        
        return userDto;
    }

    public async Task<IEnumerable<UserCleanDto>> GetByNameAsync(string firstName, string lastName, CancellationToken token = default)
    {
        var spec = new UserSpecification(user => user.FirstName == firstName && user.LastName == lastName).WithUserInfo();
        var users = await _userRepository.GetBySpecAsync(spec, token);
        var userDtos = _mapper.Map<IEnumerable<UserCleanDto>>(users);
        
        return userDtos;
    }


    public async Task<UserNecessaryDto> UpdateAsync(string id, UserNecessaryDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
        
        var userUpdated = _mapper.Map(dto, user);
        var result = await _userRepository.UpdateAsync(userUpdated);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(
                Environment.NewLine,
                result.Errors.Select(exception =>
                    exception.Description
                ));
            
            throw new IdentityException(errorMessage);
        }
        
        return _mapper.Map<UserNecessaryDto>(userUpdated);
    }

    public async Task<UserCleanDto> DeleteAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
        
        var result = await _userRepository.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(
                Environment.NewLine,
                result.Errors.Select(exception =>
                    exception.Description
                ));
            
            throw new IdentityException(errorMessage);
        }
            
        return _mapper.Map<UserCleanDto>(user);
    }
    
    public async Task<UserInfoCleanDto> DeleteUserInfoAsync(string id, CancellationToken token = default)
    {
        var user = await _userInfoRepository.GetByUserIdAsync(id, token);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserInfoNotFound);
        }
        
        await _userInfoRepository.DeleteAsync(user, token);
        
        return _mapper.Map<UserInfoCleanDto>(user);
    }

    public async Task<UserInfoDto> AddUserInfoAsync(string userId, UserInfoCleanDto dto, CancellationToken token = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
            
        var userInfo = _mapper.Map<UserInfo>(dto);
        userInfo.User = user;
        var newUserInfo = await _userInfoRepository.AddAsync(userInfo, token);
        
        return _mapper.Map<UserInfoDto>(newUserInfo);
    }
    
    public async Task<IEnumerable<UserInfoDto>> GetExpiredUserInfosAsync(CancellationToken token = default)
    {
        var spec = 
            new UserInfoSpecification(userInfo=> userInfo.LicenceExpiry <= DateTime.Now).WithUser();
        var usersInfo = await _userInfoRepository.GetBySpecAsync(spec, token);
        var userInfoDtos = _mapper.Map<IEnumerable<UserInfoDto>>(usersInfo);
        
        return userInfoDtos;
    }

    public async Task<UserInfoCleanDto> UpdateUserInfoAsync(string userId, UserInfoCleanDto dto)
    {
        var userInfo = await _userInfoRepository.GetByUserIdAsync(userId);
        
        if (userInfo == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
        
        var userUpdated = _mapper.Map(dto, userInfo);
        await _userInfoRepository.UpdateAsync(userUpdated);
        
        return _mapper.Map<UserInfoCleanDto>(userUpdated);
    }
}