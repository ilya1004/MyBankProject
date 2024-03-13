using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface IAdminRepository
{
    Task<bool> Add(Admin admin);

    Task<Admin> GetById(int id);

    Task<List<Admin>> GetAll();

    Task<bool> UpdateInfo(int id, string nickname);

    Task<bool> Delete(int id);
}