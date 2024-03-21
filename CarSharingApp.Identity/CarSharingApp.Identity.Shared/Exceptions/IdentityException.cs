namespace CarSharingApp.Identity.Shared.Exceptions;

public class IdentityException : Exception
{
    public IdentityException() { }
    public IdentityException(string message) : base(message) { }
    public IdentityException(string message, Exception innerException) : base(message, innerException) { }
}