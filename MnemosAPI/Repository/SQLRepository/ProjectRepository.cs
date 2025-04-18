using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MnemosAPI.Data;
using MnemosAPI.DTO;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.Models;
using MnemosAPI.Utilities;
using NuGet.Protocol;
using MnemosAPI.Services;

namespace MnemosAPI.Repository.SQLRepository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(MnemosDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Project>> GetAllWithForeignKeysAsync()
        {
            var projects = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .ToListAsync();

            return projects;
        }

        public async Task<Project> GetAllWithForeignKeysByIdAsync(int id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return project;
        }

        public async Task<IEnumerable<Project>> GetLatestProjectsAsync(int count)
        {
            var latestProjects = await dbContext.Projects
                .OrderByDescending(p => p.StartDate)
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Take(count) 
                .ToListAsync();

            return latestProjects;
        }


        public async Task<List<IGrouping<Customer, Project>>> GetGroupedByCustomerAsync()
        {
            var groupedByCustomer = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .GroupBy(x => x.Customer)
                .ToListAsync();

            return groupedByCustomer;
        }

        public async Task<List<IGrouping<Sector, Project>>> GetGroupedBySectorAsync()
        {
            var groupedBySector = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .GroupBy(x => x.Sector)
                .ToListAsync();

            return groupedBySector;
        }

        public async Task<List<IGrouping<Role, Project>>> GetGroupedByRoleAsync()
        {
            var groupedByRole = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .GroupBy(x => x.Role)
                .ToListAsync();

            return groupedByRole;
        }

        public async Task<List<IGrouping<EndCustomer, Project>>> GetGroupedByEndCustomerAsync()
        {
            var groupedByEndCustomer = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Where(x => x.EndCustomer != null)
                .GroupBy(x => x.EndCustomer)
                .ToListAsync();

            return groupedByEndCustomer;
        }

        public async Task<List<IGrouping<DateOnly, Project>>> GetGroupedByStartDateAsync()
        {
            var groupedByStartDate = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .GroupBy(x => x.StartDate)
                .ToListAsync();

            return groupedByStartDate;
        }

        public async Task<List<Project>> GetByInProgressStatusAsync()
        {
            var projectsInProgress = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Where(p => p.Status == StatusesEnum.INPROGRESS.ToString())
                .ToListAsync();

            return projectsInProgress;
        }
    }
}

