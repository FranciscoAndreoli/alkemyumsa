﻿using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DataAccess.Repositories;

namespace alkemyumsa.Services
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        Task<int> Complete();
    }
}