namespace CarSharingApp.CarService.Domain.Entities;

public class Comment : BaseEntity
{
    public string UserId { get; set; }
    public string CarId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
    public Car Car { get; set; }
}