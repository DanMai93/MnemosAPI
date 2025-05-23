﻿using MnemosAPI.Data;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class SectorRepository : Repository<Sector>, ISectorRepository
    {
        public SectorRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }
    }
}
