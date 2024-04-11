﻿namespace MyBank.Application.Interfaces;

public interface IModeratorsService
{
    Task<ServiceResponse<int>> Add(string login, string password, string nickname);
    Task<ServiceResponse<(int, string)>> Login(string login, string password);
    Task<ServiceResponse<Moderator>> GetById(int id);
    Task<ServiceResponse<List<Moderator>>> GetAll();
    Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname);
    Task<ServiceResponse<bool>> Delete(int id);
}
