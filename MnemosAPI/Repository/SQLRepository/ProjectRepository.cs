﻿using Microsoft.EntityFrameworkCore;
using MnemosAPI.Data;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.Models;

namespace MnemosAPI.Repository.SQLRepository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<IGrouping<Customer, Project>>> GetGroupedByCustomerAsync()
        {
            var groupedByCustomer = await dbContext.Projects
                .GroupBy(x => x.Customer)
                .ToListAsync();

            return groupedByCustomer;
        }

        public async Task<List<IGrouping<Sector, Project>>> GetGroupedBySectorAsync()
        {
            var groupedBySector = await dbContext.Projects
                .GroupBy(x => x.Sector)
                .ToListAsync();

            return groupedBySector;
        }

        public async Task<List<IGrouping<Role, Project>>> GetGroupedByRoleAsync()
        {
            var groupedByRole = await dbContext.Projects
                .GroupBy(x => x.Role)
                .ToListAsync();

            return groupedByRole;
        }

        public async Task<List<IGrouping<EndCustomer, Project>>> GetGroupedByEndCustomerAsync()
        {
            var groupedByEndCustomer = await dbContext.Projects
                .Where(x => x.EndCustomer != null)
                .GroupBy(x => x.EndCustomer)
                .ToListAsync();

            return groupedByEndCustomer;
        }

        public async Task<List<IGrouping<DateOnly, Project>>> GetGroupedByStartDateAsync()
        {
            var groupedByStartDate = await dbContext.Projects
                .GroupBy(x => x.StartDate)
                .ToListAsync();

            return groupedByStartDate;
        }
    }
}

