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
        CreateMap<CreateDealCommand, Deal>()
            .ForMember(
                dest => dest.CarId, 
                opt => 
                    opt.MapFrom(src =>src.CarId))
            .ForMember(
                dest => dest.TotalPrice, 
                opt => 
                    opt.MapFrom(src =>src.TotalPrice))
            .ForMember(
                dest => dest.UserId, 
                opt =>
                    opt.MapFrom(src =>src.UserId));
        CreateMap<Deal, DealDto>()
            .ForMember(
                dest => dest.Finished, 
                opt => 
                    opt.MapFrom(src =>src.Finished))
            .ForMember(
                dest => dest.Id, 
                opt => 
                    opt.MapFrom(src =>src.Id))
            .ForMember(
                dest => dest.Rating, 
                opt => 
                    opt.MapFrom(src =>src.Rating))
            .ForMember(
                dest => dest.State, 
                opt => 
                    opt.MapFrom(src =>src.State))
            .ForMember(
                dest => dest.CarId, 
                opt => 
                    opt.MapFrom(src =>src.CarId))
            .ForMember(
                dest => dest.TotalPrice, 
                opt => 
                    opt.MapFrom(src =>src.TotalPrice))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.Requested, 
                opt =>
                    opt.MapFrom(src =>src.Requested));

        CreateMap<CreateFeedbackCommand, Feedback>()
            .ForMember(
                dest => dest.Text,
                opt =>
                    opt.MapFrom(src => src.Text))
            .ForMember(
                dest => dest.Rating,
                opt =>
                    opt.MapFrom(src => src.Rating))
            .ForMember(
                dest => dest.DealId,
                opt =>
                    opt.MapFrom(src => src.DealId))
            .ForMember(
                dest => dest.IssueType,
                opt =>
                    opt.MapFrom(src => src.IssueType))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId));
        CreateMap<Feedback, FeedbackDto>()
            .ForMember(
                dest => dest.Posted, 
                opt => 
                    opt.MapFrom(src =>src.Posted))
            .ForMember(
                dest => dest.Rating, 
                opt => 
                    opt.MapFrom(src =>src.Rating))
            .ForMember(
                dest => dest.Text, 
                opt => 
                    opt.MapFrom(src =>src.Text))
            .ForMember(
                dest => dest.IsChanged, 
                opt => 
                    opt.MapFrom(src =>src.IsChanged))
            .ForMember(
                dest => dest.Id, 
                opt => 
                    opt.MapFrom(src =>src.Id))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.IssueType, 
                opt =>
                    opt.MapFrom(src =>src.IssueType));

        CreateMap<CreateAnswerCommand, Answer>()
            .ForMember(
                dest => dest.Text, 
                opt => 
                    opt.MapFrom(src =>src.Text))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.FeedBackId, 
                opt =>
                    opt.MapFrom(src =>src.FeedBackId));
        CreateMap<Answer, AnswerDto>()
            .ForMember(
                dest => dest.Text, 
                opt => 
                    opt.MapFrom(src =>src.Text))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.FeedBackId, 
                opt =>
                    opt.MapFrom(src =>src.FeedBackId))
            .ForMember(
                dest => dest.Posted, 
                opt => 
                    opt.MapFrom(src =>src.Posted))
            .ForMember(
                dest => dest.IsChanged, 
                opt => 
                    opt.MapFrom(src =>src.IsChanged))
            .ForMember(
                dest => dest.Id, 
                opt =>
                    opt.MapFrom(src =>src.Id));
    }
}