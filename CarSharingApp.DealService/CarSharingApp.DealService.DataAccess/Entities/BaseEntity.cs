namespace CarSharingApp.DealService.DataAccess.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
}