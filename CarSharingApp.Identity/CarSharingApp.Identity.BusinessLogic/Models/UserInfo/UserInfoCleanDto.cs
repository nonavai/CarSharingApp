using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.BusinessLogic.Models.UserInfo;

public class UserInfoCleanDto
{
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LicenceType Category { get; set; }
    public DateTime LicenceExpiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
}