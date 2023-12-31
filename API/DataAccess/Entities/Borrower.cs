﻿using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace DataAccess.Entities;

public class Borrower : EntityBase
{
    public int UserId { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string FirstName { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string LastName { get; set; }
    public LicenceCategory Category { get; set; }
    public DateTime LicenceExpiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    [Column(TypeName = "nvarchar(15)")]
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
    //some info that needed from driver 
    public User User { get; set; }
    public IEnumerable<Deal> Deals { get; set; }
    public Roles Roles { get; set; }
}