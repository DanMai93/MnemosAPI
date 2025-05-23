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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public async Task<int> UpdateProjectAsync( int projectId, JsonPatchDocument<UpdateProjectRequestDto> patchDoc, ModelStateDictionary modelState)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
            if (existingProject == null)
                throw new ArgumentException("Progetto non trovato");

            // Step 1: Map entity to DTO
            var projectDto = new UpdateProjectRequestDto
            {
                Title = existingProject.Title,
                CustomerId = (int)existingProject.CustomerId,
                EndCustomerId = existingProject.EndCustomerId,
                StartDate = existingProject.StartDate,
                EndDate = existingProject.EndDate,
                Description = existingProject.Description,
                WorkOrder = existingProject.WorkOrder,
                RoleId = existingProject.RoleId,
                SectorId = existingProject.SectorId,
                JobCode = existingProject.JobCode,
                UserId = existingProject.UserId,
                Difficulty = existingProject.Difficulty != "" ? Enum.Parse<DifficultiesEnum>(existingProject.Difficulty) : null,
                Status = existingProject.Status != "" ? Enum.Parse<StatusesEnum>(existingProject.Status) : null,
                Goals = existingProject.Goals,
                Repository = existingProject.Repository,
                GoalSolutions = existingProject.GoalSolutions,
                SolutionsImpact = existingProject.SolutionsImpact,
                BusinessUnitId = existingProject.BusinessUnitId,
                Skills = existingProject.Skills.Select(x => x.Id).ToArray(),
                Architectures = existingProject.Architectures.Select(x => x.Id).ToArray(),
                WorkMethods = existingProject.WorkMethods.Select(x => x.Id).ToArray(),
                ManagementTools = existingProject.ManagementTools.Select(x => x.Id).ToArray(),
                SoftSkills = existingProject.SoftSkills.Select(x => x.Id).ToArray()
            };

            
            patchDoc.ApplyTo(projectDto, modelState);
            if (!modelState.IsValid)
                throw new InvalidOperationException("Model state invalid");

            //  Map DTO back to entity
            existingProject.Title = projectDto.Title;
            existingProject.CustomerId = projectDto.CustomerId;
            existingProject.EndCustomerId = projectDto.EndCustomerId;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.Description = projectDto.Description;
            existingProject.WorkOrder = projectDto.WorkOrder;
            existingProject.RoleId = projectDto.RoleId;
            existingProject.SectorId = projectDto.SectorId;
            existingProject.JobCode = projectDto.JobCode;
            existingProject.UserId = projectDto.UserId;
            existingProject.Difficulty = projectDto.Difficulty?.ToString();
            existingProject.Status = projectDto.Status?.ToString();
            existingProject.Goals = projectDto.Goals;
            existingProject.Repository = projectDto.Repository;
            existingProject.GoalSolutions = projectDto.GoalSolutions;
            existingProject.SolutionsImpact = projectDto.SolutionsImpact;
            existingProject.BusinessUnitId = projectDto.BusinessUnitId;

            // If you are modifying navigation collections, you need to manage them explicitly:
            existingProject.Skills = dbContext.Skills.Where(s => projectDto.Skills.Contains(s.Id)).ToList();
            existingProject.Architectures = dbContext.Architectures.Where(a => projectDto.Architectures.Contains(a.Id)).ToList();
            existingProject.WorkMethods = dbContext.WorkMethods.Where(w => projectDto.WorkMethods.Contains(w.Id)).ToList();
            existingProject.ManagementTools = dbContext.ManagementTools.Where(m => projectDto.ManagementTools.Contains(m.Id)).ToList();
            existingProject.SoftSkills = dbContext.SoftSkills.Where(s => projectDto.SoftSkills.Contains(s.Id)).ToList();

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

