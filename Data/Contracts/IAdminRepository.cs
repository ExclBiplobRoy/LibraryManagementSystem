﻿using Domain.Entities;

namespace Data.Contracts
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<Admin> GetAdminByKey(int key);

        public Task<Admin> GetAdminByEmail(string key);

        public Task<IEnumerable<Admin>> GetAdmins();
    }
}