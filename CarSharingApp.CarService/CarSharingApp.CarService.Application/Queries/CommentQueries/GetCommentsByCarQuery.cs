using CarSharingApp.CarService.Application.DTO_s.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CommentQueries;

public class GetCommentsByCarQuery : IRequest<IEnumerable<CommentDto>>
{
    public string CarId { get; set; }
}