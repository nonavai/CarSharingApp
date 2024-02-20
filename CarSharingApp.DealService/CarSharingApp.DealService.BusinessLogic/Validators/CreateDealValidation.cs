using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using FluentValidation;

namespace CarSharingApp.DealService.BusinessLogic.Validators;

public class CreateDealValidation : AbstractValidator<CreateDealCommand>
{
    public CreateDealValidation()
    {
    }
}