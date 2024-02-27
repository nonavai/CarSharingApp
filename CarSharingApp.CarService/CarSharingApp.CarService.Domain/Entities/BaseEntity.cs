namespace CarSharingApp.CarService.Domain.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
}