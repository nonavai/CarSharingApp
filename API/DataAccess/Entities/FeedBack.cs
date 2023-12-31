namespace DataAccess.Entities;

public class FeedBack : EntityBase
{
    public int CarId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }

    public Car Car { get; set; }
    public User User { get; set; }
}