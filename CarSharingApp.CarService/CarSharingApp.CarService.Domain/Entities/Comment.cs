namespace CarSharingApp.CarService.Domain.Entities;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
    public Car Car { get; set; }
}