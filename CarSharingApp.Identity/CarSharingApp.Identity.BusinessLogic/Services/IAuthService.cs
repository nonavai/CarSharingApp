using CarSharingApp.Identity.BusinessLogic.Models.User;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IAuthService
{
    Task<UserAuthorizedDto> LogInAsync(LogInDto dto);
    Task RegistrationAsync(UserNecessaryDto dto);
}