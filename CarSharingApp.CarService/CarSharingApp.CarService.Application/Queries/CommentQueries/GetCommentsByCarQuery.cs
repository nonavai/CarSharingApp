using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CommentQueries;

public class GetCommentsByCarQuery : IRequest<IEnumerable<CommentResponse>>
{
    public string CarId { get; set; }
}