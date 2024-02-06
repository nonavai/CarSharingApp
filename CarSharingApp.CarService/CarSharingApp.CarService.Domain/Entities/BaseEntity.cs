namespace CarSharingApp.CarService.Domain.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = new Guid().ToString();
    }

    public string Id { get; set; }
}