﻿using System.Data;
using AutoMapper;
using BusinessLogic.Models.Deal;
using DataAccess.Entities;
using DataAccess.Repositories;
using Shared.Enums;
using Shared.Exceptions;

namespace BusinessLogic.Services.Implemetation;

public class DealService : IDealService
{
    private readonly ICarService _carService;
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public DealService(IMapper mapper, IDealRepository dealRepository, ICarService carService)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
        _carService = carService;
    }
    
    public async Task<DealDto> GetByIdAsync(int id)
    {
        var deal = await _dealRepository.GetByIdAsync(id);
        if (deal == null)
        {
            throw new NotFoundException("Deal not found");
        }
        var dealDto = _mapper.Map<DealDto>(deal);
        return dealDto;
    }

    public async Task<DealDto> RegisterDealAsync(DealDto dealDto)
    {
        var deal = _mapper.Map<Deal>(dealDto);
        await _carService.SetUnactive(dealDto.Id, false);
        var newDeal = await _dealRepository.AddAsync(deal);
        var newDealDto = _mapper.Map<DealDto>(newDeal);
        newDealDto.State = DealState.Active;
        newDealDto.BookingStart = DateTime.UtcNow;
        newDealDto.BookingEnd = DateTime.UtcNow;
        newDealDto.TotalPrice = 0;
        return newDealDto;
    }

    public async Task CancelDealAsync(int id)
    {
        var deal = await GetByIdAsync(id);
        await _carService.SetUnactive(deal.CarId, false);
        if (deal.TotalPrice > 0) return;
        var deletedDeal = await _dealRepository.DeleteAsync(id);
    }

    public async Task<DealDto> ConfirmDealAsync(DealDto entity)
    {
        var existingDeal = await _dealRepository.GetByIdAsync(entity.Id);
        if (existingDeal == null)
        {
            throw new NotFoundException("User not found");
        }
        
        existingDeal.State = entity.State;
        existingDeal.BookingStart = entity.BookingStart;
        existingDeal.BookingEnd = entity.BookingEnd;
        existingDeal.TotalPrice = entity.TotalPrice;


        var dealDto = _mapper.Map<DealDto>(await _dealRepository.UpdateAsync(existingDeal));
        return dealDto;
    }

    public async Task<DealDto> RateDealAsync(int id, int raiting)
    {
        var existingDeal = await _dealRepository.GetByIdAsync(id);
        if (existingDeal == null)
        {
            throw new NotFoundException("User not found");
        }
        await _carService.SetUnactive(existingDeal.CarId, false);
        existingDeal.Raiting = raiting;
        var dealDto = _mapper.Map<DealDto>(await _dealRepository.UpdateAsync(existingDeal));
        return dealDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dealRepository.ExistsAsync(id);
    }
    
    
    

}