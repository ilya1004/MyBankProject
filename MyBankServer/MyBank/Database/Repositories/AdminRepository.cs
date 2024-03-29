﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public AdminRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> Add(Admin admin)
    {
        var adminEntity = new AdminEntity
        {
            Id = 0,
            Login = admin.Login,
            HashedPassword = admin.HashedPassword,
            Nickname = admin.Nickname,
            Messages = []
        };

        await _dbContext.Admins.AddAsync(adminEntity);
        var number = await _dbContext.SaveChangesAsync();
        return number == 0 ? false : true;
    }

    public async Task<Admin> GetById(int id)
    {
        var adminEntity = await _dbContext.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(а => а.Id == id);

        return _mapper.Map<Admin>(adminEntity);
    }

    public async Task<List<Admin>> GetAll()
    {
        var adminEntitiesList = await _dbContext.Admins
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Admin>>(adminEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string nickname)
    {
        var number = await _dbContext.Admins
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Nickname, nickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Admins
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
