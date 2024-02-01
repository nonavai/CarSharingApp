using AutoMapper;
using CarSharingApp.Identity.BusinessLogic.Models.Role;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Exceptions;

namespace CarSharingApp.Identity.BusinessLogic.Services.Implementations;

public class RolesService : IRolesService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RolesService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDto>> GetUserRolesAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }

        var roles = await _userRepository.GetRolesAsync(user);
        var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);

        return roleDtos;
    }

    public async Task AddUserRoleAsync(string id, string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }

        switch (role)
        {
            case RoleNames.Lender:
                await _userRepository.AddToRoleAsync(user, RoleNames.Lender);
                break;
            
            case RoleNames.Borrower:
                await _userRepository.AddToRoleAsync(user, RoleNames.Borrower);
                break;
            
            case RoleNames.Admin:
                await _userRepository.AddToRoleAsync(user, RoleNames.Admin);
                break;
            
            default:
                throw new NotFoundException(ErrorName.RoleNotFound);
        }
    }

    public async Task RemoveUserRoleAsync(string id, string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }

        switch (role)
        {
            case RoleNames.Lender:
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Lender);
                break;
            
            case RoleNames.Borrower:
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Borrower);
                break;
            
            case RoleNames.Admin:
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Admin);
                break;
            
            default:
                throw new NotFoundException(ErrorName.RoleNotFound);
        }
    }
}