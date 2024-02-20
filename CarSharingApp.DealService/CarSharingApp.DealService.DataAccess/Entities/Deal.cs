using CarSharingApp.DealService.Shared.Enums;

namespace CarSharingApp.DealService.DataAccess.Entities;

public class Deal : BaseEntity
{
    public string CarId { get; set; }
    public string UserId { get; set; }
    public DateTime Requested { get; set; }
    public DateTime? Finished { get; set; }
    public DealState State { get; set; }
    public float TotalPrice { get; set; }
    public int? Rating { get; set; }
}