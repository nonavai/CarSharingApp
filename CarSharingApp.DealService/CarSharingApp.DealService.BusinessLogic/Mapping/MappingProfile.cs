using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.DataAccess.Entities;

namespace CarSharingApp.DealService.BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDealCommand, Deal>();
        CreateMap<Deal, DealDto>();

        CreateMap<CreateFeedbackCommand, Feedback>();
        CreateMap<Feedback, FeedbackDto>();

        CreateMap<CreateAnswerCommand, Answer>();
        CreateMap<Answer, AnswerDto>();
    }
}