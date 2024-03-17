using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.BusinessLogic.Models.UserInfo;

public class UserInfoDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public LicenceType Category { get; set; }
    public DateTime LicenceExpiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
    public virtual UserCleanDto User { get; set; }
}