namespace CarSharingApp.DealService.Shared.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("Cant Execute This Operation"){ }
    public BadRequestException(Exception innerException) : base("Cant Execute This Operation", innerException) { }
}