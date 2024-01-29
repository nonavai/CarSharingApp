using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Enums;
using CarSharingApp.Identity.Shared.Exceptions;

namespace CarSharingApp.Identity.BusinessLogic.Services.Implementations;

public class RolesService : IRolesService
{
    private readonly IUserRepository _userRepository;

    public RolesService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
                await _userRepository.AddToRoleAsync(user, RoleNames.Lender);
                break;
            
            case Roles.Borrower:
                await _userRepository.AddToRoleAsync(user, RoleNames.Borrower);
                break;
            
            case Roles.Admin:
                await _userRepository.AddToRoleAsync(user, RoleNames.Admin);
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
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Lender, token);
                break;
            
            case Roles.Borrower:
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Borrower, token);
                break;
            
            case Roles.Admin:
                await _userRepository.RemoveFromRolesAsync(user, RoleNames.Admin, token);
                break;
            
            default:
                throw new NotFoundException("Role Not Found");
        }
    }
}