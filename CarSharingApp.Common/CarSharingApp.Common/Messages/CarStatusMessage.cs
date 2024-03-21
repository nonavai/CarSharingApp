using CarSharingApp.Common.Enums;

namespace CarSharingApp.Common.Messages;

public class CarStatusMessage
{
    public string CarId { get; set; }
    public Status Status { get; set; }
}