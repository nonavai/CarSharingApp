using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CarSharingApp.Identity.DataAccess.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    [Column(TypeName = "nvarchar(30)")]
    public string RecordNumber { get; set; }
    public virtual UserInfo UserInfo { get; set; }
}