﻿using AutoMapper;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;

namespace CarSharingApp.CarService.Application.CommandHandlers.ImageCommandHandlers;

public class UpdateImagePriorityHandler: ICommandHandler<UpdateImagePriorityCommand, ImageDto>
{
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;
    
    
    public UpdateImagePriorityHandler(ICarImageRepository carImageRepository, IMapper mapper)
    {
        _carImageRepository = carImageRepository;
        _mapper = mapper;
    }

    public async Task<ImageDto> Handle(UpdateImagePriorityCommand command)
    {
        var newPrimaryImage = await _carImageRepository.GetByIdAsync(command.Id);

        if (newPrimaryImage == null)
        {
            throw new NotFoundException("Image Not Found");
        }
        
        var oldPrimaryImage = await _carImageRepository.GetPrimaryAsync();
        newPrimaryImage.IsPrimary = true;
        var updatedNewImage = await _carImageRepository.UpdateAsync(newPrimaryImage);
        var updatedImageDto = _mapper.Map<ImageDto>(updatedNewImage);
        
        if (oldPrimaryImage != null)
        {
            oldPrimaryImage.IsPrimary = false;
            var updatedOldImage = await _carImageRepository.UpdateAsync(oldPrimaryImage);
        }

        return updatedImageDto;
    }
}