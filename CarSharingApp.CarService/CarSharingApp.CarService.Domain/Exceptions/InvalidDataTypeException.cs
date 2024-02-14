namespace CarSharingApp.CarService.Domain.Exceptions;

public class InvalidDataTypeException : Exception
{
    public InvalidDataTypeException() { }
    public InvalidDataTypeException(string message) : base("Invalid Data Type For " + message) { }
    public InvalidDataTypeException(string message, Exception innerException) : base("Invalid Data Type For " + message, innerException) { }
}