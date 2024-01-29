namespace CarSharingApp.CarService.Domain.Entities;

public class Picture : BaseEntity
{
    public Guid id { get; set; }
    public Guid CarId { get; set; }
    public bool IsPrimary { get; set; }
    public string Url { get; set; }
    public Car Car { get; set; }
}