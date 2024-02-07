namespace CarSharingApp.Identity.Shared.Exceptions;

public class BadAuthorizeException : Exception
{
    public BadAuthorizeException() { }
    public BadAuthorizeException(string message) : base(message) { }
    public BadAuthorizeException(string message, Exception innerException) : base(message, innerException) { }
}