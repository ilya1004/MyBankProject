﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class DepositAccrualsRepository : IDepositAccrualsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public DepositAccrualsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(DepositAccrual depositAccrual, int depositAccountId)
    {
        var depositAccountEntity = await _dbContext.DepositAccounts
            .FirstOrDefaultAsync(da => da.Id == depositAccountId);

        var depositAccrualEntity = new DepositAccrualEntity
        {
            Id = 0,
            AccrualAmount = depositAccrual.AccrualAmount,
            Datetime = depositAccrual.Datetime,
            Status = depositAccrual.Status,
            DepositAccountId = depositAccountId,
            DepositAccount = depositAccountEntity,
        };

        var item = await _dbContext.DepositAccounts.AddAsync(depositAccountEntity!);
        var number = await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<DepositAccrual> GetById(int id)
    {
        var depositAccrualEntity = await _dbContext.DepositAccruals
            .AsNoTracking()
            .FirstOrDefaultAsync(da => da.Id == id);

        return _mapper.Map<DepositAccrual>(depositAccrualEntity);
    }

    public async Task<List<DepositAccrual>> GetAllByDepositId(int depositId)
    {
        var depositAccrualEntitiesList = await _dbContext.DepositAccruals
            .AsNoTracking()
            .Where(da => da.DepositAccountId == depositId)
            .ToListAsync();

        return _mapper.Map<List<DepositAccrual>>(depositAccrualEntitiesList);
    }
}
