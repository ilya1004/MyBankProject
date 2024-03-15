using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class DepositAccrualsService : IDepositAccrualsService
{
    private readonly IDepositAccrualsRepository _depositAccrualsRepository;

    public DepositAccrualsService(IDepositAccrualsRepository depositAccrualsRepository)
    {
        _depositAccrualsRepository = depositAccrualsRepository;
    }

    public async Task<int> Add()  // TODO
    {
        var depositAccountId = 0;

        var depositAccrual = new DepositAccrual(0, 0, DateTime.UtcNow, DepositAccrualStatus.Ok);

        return await _depositAccrualsRepository.Add(depositAccrual, depositAccountId);
    }

    public async Task<DepositAccrual> GetById(int id)
    {
        return await _depositAccrualsRepository.GetById(id);
    }

    public async Task<List<DepositAccrual>> GetAllByDepositId(int depositAccountId)
    {
        return await _depositAccrualsRepository.GetAllByDepositId(depositAccountId);
    }
}
