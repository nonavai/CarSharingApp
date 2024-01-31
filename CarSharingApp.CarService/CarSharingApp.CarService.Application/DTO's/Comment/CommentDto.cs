using CarSharingApp.CarService.Application.DTO_s.Car;

namespace CarSharingApp.CarService.Application.DTO_s.Comment;

public class CommentDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CarId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
    public CarDto Car { get; set; }
}