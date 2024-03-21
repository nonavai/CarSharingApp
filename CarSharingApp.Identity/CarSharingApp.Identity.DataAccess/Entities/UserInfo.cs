using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.DataAccess.Entities;

public class UserInfo 
{
    public UserInfo()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public LicenceType Category { get; set; }
    public DateTime LicenceExpiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
    public virtual User User { get; set; }
}