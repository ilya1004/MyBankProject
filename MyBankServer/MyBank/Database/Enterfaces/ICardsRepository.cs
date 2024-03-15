﻿using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface ICardsRepository
{
    Task<int> Add(Card card, int cardPackageId, int userId, int personalAccountId);

    Task<Card> GetById(int id);

    Task<Card> GetByNumber(string number);

    Task<List<Card>> GetAllByUser(int userId);

    Task<bool> UpdatePincode(int id, string pincode);

    Task<bool> UpdateName(int id, string name);

    Task<bool> UpdateStatus(int id, bool isActive);

    Task<bool> Delete(int id);
}