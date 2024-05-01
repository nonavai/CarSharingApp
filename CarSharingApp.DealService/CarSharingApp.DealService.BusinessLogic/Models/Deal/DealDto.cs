using CarSharingApp.DealService.Shared.Enums;

namespace CarSharingApp.DealService.BusinessLogic.Models.Deal;

public class DealDto
{
    public string Id { get; set; }
    public string CarId { get; set; }
    public string UserId { get; set; }
    public DateTime Requested { get; set; }
    public DateTime? Finished { get; set; }
    public DealState State { get; set; }
    public float TotalPrice { get; set; }
    public int? Rating { get; set; }
}