namespace CarSharingApp.Identity.Shared.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}