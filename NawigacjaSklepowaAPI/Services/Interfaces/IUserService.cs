﻿using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
    }
}
