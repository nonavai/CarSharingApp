using Microsoft.AspNetCore.Identity;

namespace CarSharingApp.Identity.DataAccess.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string RecordNumber { get; set; }
    public virtual UserInfo UserInfo { get; set; }
}