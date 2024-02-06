using CarSharingApp.CarService.Application.DTO_s.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CommentQueries;

public class GetCommentQuery : IRequest<CommentDto>
{
    public string Id { get; set; }
}