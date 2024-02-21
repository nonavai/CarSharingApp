using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;

namespace CarSharingApp.Identity.BusinessLogic.Models.User;

public class UserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string RecordNumber { get; set; }
    public string UserName { get; set; }
    public UserInfoDto UserInfoDto { get; set; }
}