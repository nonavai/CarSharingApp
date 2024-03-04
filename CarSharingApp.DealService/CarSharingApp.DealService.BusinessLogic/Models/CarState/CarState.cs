using CarSharingApp.DealService.Shared.Enums;

namespace CarSharingApp.DealService.BusinessLogic.Models.CarState;

public class CarState
{
    public string CarId { get; set; }
    public CarStatus Status { get; set; }
}