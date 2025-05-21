using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MnemosAPI.Data;
using MnemosAPI.DTO;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.Models;
using MnemosAPI.Utilities;
using NuGet.Protocol;
using MnemosAPI.Services;
using MnemosAPI.DTO.UpdateRequestDto;

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
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(p => p.EndCustomer).Include(s => s.Skills)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .ToListAsync();

            return projects;
        }

        public async Task<Project> GetByIdWithForeignKeysAsync(int id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .FirstOrDefaultAsync(x => x.Id == id);

            return project;
        }

        public async Task<IEnumerable<Project>> GetLatestProjectsAsync(int count)
        {
            var latestProjects = await dbContext.Projects
                .OrderByDescending(p => p.StartDate)
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .Take(count)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();

            return latestProjects;
        }


        public async Task<List<IGrouping<Customer, Project>>> GetGroupedByCustomerAsync()
        {
            var groupedByCustomer = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .GroupBy(x => x.Customer)
                .ToListAsync();

            return groupedByCustomer;
        }

        public async Task<List<IGrouping<Sector, Project>>> GetGroupedBySectorAsync()
        {
            var groupedBySector = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .GroupBy(x => x.Sector)
                .ToListAsync();

            return groupedBySector;
        }

        public async Task<List<IGrouping<Role, Project>>> GetGroupedByRoleAsync()
        {
            var groupedByRole = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .GroupBy(x => x.Role)
                .ToListAsync();

            return groupedByRole;
        }

        public async Task<List<IGrouping<EndCustomer, Project>>> GetGroupedByEndCustomerAsync()
        {
            var groupedByEndCustomer = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .Where(x => x.EndCustomer != null)
                .GroupBy(x => x.EndCustomer)
                .ToListAsync();

            return groupedByEndCustomer;
        }

        public async Task<List<IGrouping<DateOnly?, Project>>> GetGroupedByStartDateAsync()
        {
            var groupedByStartDate = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .GroupBy(x => x.StartDate)
                .ToListAsync();

            return groupedByStartDate;
        }

        public async Task<List<Project>> GetByInProgressStatusAsync()
        {
            var projectsInProgress = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .Where(p => p.Status == StatusesEnum.INPROGRESS.ToString())
                .ToListAsync();

            return projectsInProgress;
        }

        public async Task<int> UpdateProjectAsync(int id, Project project)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if(existingProject == null)
            {
                throw new ArgumentException("Progetto non trovato" );
            }

            existingProject.Title = project.Title;
            existingProject.CustomerId = project.CustomerId;
            existingProject.EndCustomerId = project.EndCustomerId;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Description = project.Description;
            existingProject.WorkOrder = project.WorkOrder;
            existingProject.RoleId = project.RoleId;
            existingProject.SectorId = project.SectorId;
            existingProject.JobCode = project.JobCode;
            existingProject.UserId = project.UserId;
            existingProject.Difficulty = project.Difficulty.ToString();
            existingProject.Status = project.Status.ToString();
            existingProject.Goals = project.Goals;
            existingProject.Repository = project.Repository;
            existingProject.GoalSolutions = project.GoalSolutions;
            existingProject.SolutionsImpact = project.SolutionsImpact;
            existingProject.BusinessUnitId = project.BusinessUnitId;
            existingProject.Skills = project.Skills;
            existingProject.Architectures = project.Architectures;
            existingProject.WorkMethods = project.WorkMethods;
            existingProject.ManagementTools = project.ManagementTools;
            existingProject.SoftSkills = project.SoftSkills;

            await dbContext.SaveChangesAsync();

            return existingProject.Id;

        }

        public async Task<List<Project>> GetByInputStringAsync(string inputString)
        {
            var projects = await dbContext.Projects
                .Include(p => p.Sector).Include(p => p.Role).Include(p => p.User).Include(p => p.Customer).Include(s => s.Skills).Include(p => p.EndCustomer)
                .Include(p => p.BusinessUnit).Include(p => p.Architectures).Include(p => p.WorkMethods).Include(p => p.ManagementTools).Include(p => p.SoftSkills)
                .Where(p => p.Title.ToLower().Contains(inputString.ToLower()))
                .OrderBy(x => x.Id)
                .ToListAsync();

            return projects;
        }
    }
}

