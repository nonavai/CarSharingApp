using AutoMapper;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.DataAccess.Specifications;
using CarSharingApp.Identity.Shared.Exceptions;
using Roles = CarSharingApp.Identity.Shared.Enums.Roles;

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

    public async Task<UserDto> LogInAsync(LogInDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        
        if (user == null)
        {
            throw new NotFoundException("Email Not Found");
        }

        if (!await _userRepository.CheckPasswordAsync(user, dto.Password))
        {
            throw new BadAuthorizeException("Invalid Password");
        }

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task RegistrationAsync(UserNecessaryDto dto, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        if (await IsEmailExist(dto.Email))
        {
            throw new BadAuthorizeException("User already exist");
        }
        
        var user = _mapper.Map<User>(dto);
        var result = await _userRepository.AddAsync(user, dto.Password, token);

        if (!result.Succeeded)
        {
            throw new IdentityException("Cannot Add New User");
        }
    }

    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }
        
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<IEnumerable<UserCleanDto>> GetByNameAsync(string firstName, string lastName, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
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
            throw new NotFoundException("User Not Found");
        }
        
        var userUpdated = _mapper.Map(dto, user);
        var result = await _userRepository.UpdateAsync(userUpdated);

        if (!result.Succeeded)
        {
            throw new IdentityException("Cannot Update User");
        }
        
        return _mapper.Map<UserNecessaryDto>(userUpdated);
    }

    public async Task<UserCleanDto> DeleteAsync(string id, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }
        
        var result = await _userRepository.DeleteAsync(user, token);

        if (!result.Succeeded)
        {
            throw new IdentityException("Cannot Delete User");
        }
            
        return _mapper.Map<UserCleanDto>(user);
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }
        
        return await _userRepository.GetRolesAsync(user);
    }

    public async Task AddUserRoleAsync(string id, Roles role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }

        switch (role)
        {
            case Roles.Lender:
                await _userRepository.AddToRoleAsync(user, "lender");
                break;
            
            case Roles.Borrower:
                await _userRepository.AddToRoleAsync(user, "borrower");
                break;
            
            case Roles.Admin:
                await _userRepository.AddToRoleAsync(user, "admin");
                break;
            
            default:
                throw new NotFoundException("Role Not Found");
        }
    }

    public async Task RemoveUserRoleAsync(string id, Roles role, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }

        switch (role)
        {
            case Roles.Lender:
                await _userRepository.RemoveFromRolesAsync(user, "lender", token);
                break;
            
            case Roles.Borrower:
                await _userRepository.RemoveFromRolesAsync(user, "borrower", token);
                break;
            
            case Roles.Admin:
                await _userRepository.RemoveFromRolesAsync(user, "admin", token);
                break;
            
            default:
                throw new NotFoundException("Role Not Found");
        }
    }

    public async Task<UserInfoDto> AddUserInfoAsync(string userId, UserInfoCleanDto dto, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User Not Found");
        }
            
        var userInfo = _mapper.Map<UserInfo>(dto);
        userInfo.User = user;
        var newUserInfo = await _userInfoRepository.AddAsync(userInfo, token);
        return _mapper.Map<UserInfoDto>(newUserInfo);
    }
    
    public async Task<IEnumerable<UserInfoDto>> GetExpiredUserInfosAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        var spec = 
            new UserInfoSpecification(userInfo=> userInfo.LicenceExpiry <= DateTime.Now).WithUser();
        var usersInfo = await _userInfoRepository.GetBySpecAsync(spec, token);
        var userInfoDtos = _mapper.Map<IEnumerable<UserInfoDto>>(usersInfo);
        return userInfoDtos;
    }

    public async Task<UserInfoCleanDto> UpdateUserInfoAsync(string userId, UserInfoCleanDto dto)
    {
        var userInfo = await _userInfoRepository.GetByUserId(userId);
        
        if (userInfo == null)
        {
            throw new NotFoundException("User Not Found");
        }
        
        var userUpdated = _mapper.Map(dto, userInfo);
        await _userInfoRepository.UpdateAsync(userUpdated);
        return _mapper.Map<UserInfoCleanDto>(userUpdated);
    }

    private async Task<bool> IsEmailExist(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user != null;
    }
    
}