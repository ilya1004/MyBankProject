using MyBank.Application.Utils;
using MyBank.Domain.DataTransferObjects.ModeratorDtos;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface IModeratorsService
{
    Task<ServiceResponse<int>> Add(RegisterModeratorDto registerModeratorDto);
    Task<ServiceResponse<Moderator>> GetById(int id);
    Task<ServiceResponse<string>> Login(string login, string password);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname);
}