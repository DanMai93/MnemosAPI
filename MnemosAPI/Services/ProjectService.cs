using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Build.ObjectModelRemoting;
using DocumentFormat.OpenXml.InkML;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.Models;
using MnemosAPI.Repository;
using MnemosAPI.Utilities;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace MnemosAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, ISkillRepository skillRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<ProjectDto> CreateProjectAsync(AddProjectRequestDto addProjectRequestDto)
        {
            var project = new Project
            {
                Title = addProjectRequestDto.Title,
                CustomerId = addProjectRequestDto.CustomerId,
                EndCustomerId = addProjectRequestDto.EndCustomerId,
                StartDate = addProjectRequestDto.StartDate,
                EndDate = addProjectRequestDto.EndDate,
                Description = addProjectRequestDto.Description,
                WorkOrder = addProjectRequestDto.WorkOrder,
                RoleId = addProjectRequestDto.RoleId,
                SectorId = addProjectRequestDto.SectorId,
                JobCode = addProjectRequestDto.JobCode,
                UserId = addProjectRequestDto.UserId,
                Difficulty = addProjectRequestDto.Difficulty.ToString(),
                Status = addProjectRequestDto.Status.ToString(),
                Goals = addProjectRequestDto.Goals,
                Repository = addProjectRequestDto.Repository,
                GoalSolutions = addProjectRequestDto.GoalSolutions,
                SolutionsImpact = addProjectRequestDto.SolutionsImpact,
                BusinessUnitId = addProjectRequestDto.BusinessUnitId
            };

            foreach (var skillId in addProjectRequestDto.Skills)
            {
                // Retrieve the full Skill entity from the database
                var skill = await _skillRepository.GetByIdAsync(skillId);
                if (skill != null)
                {
                    project.Skills.Add(skill);
                }
            }

            project = await _projectRepository.AddAsync(project);

            var addedProject = await _projectRepository.GetByIdWithForeignKeysAsync(project.Id);

            var projectDto = new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Customer = new CustomerDto() { Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                EndCustomer = new EndCustomerDto() { Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                WorkOrder = project.WorkOrder,
                Role = new RoleDto() { Title = project.Role.Title, Notes = project.Role.Notes },
                Sector = new SectorDto() { Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                JobCode = project.JobCode,
                User = new UserDto() { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Status = Enum.Parse<StatusesEnum>(project.Status),
                Goals = project.Goals,
                Repository = project.Repository,
                GoalSolutions = project.GoalSolutions,
                SolutionsImpact = project.SolutionsImpact,
                BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }
            };

            return projectDto;
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteByIdAsync(projectId);

        }

        public async Task<ProjectDto?> GetProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdWithForeignKeysAsync(projectId);

            if(project == null)
            {
                return null;
            }
            
                var projectDto = new ProjectDto()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Customer = new CustomerDto() { Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                    EndCustomer = new EndCustomerDto() { Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = new RoleDto() { Title = project.Role.Title, Notes = project.Role.Notes },
                    Sector = new SectorDto() { Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = new UserDto() { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }
                };
            
           
            return projectDto;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync()
        {
            var projectList = await _projectRepository.GetAllWithForeignKeysAsync();

            var projectListDto = new List<ProjectDto>();

            foreach (var project in projectList)
            {
                projectListDto.Add(new ProjectDto()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Customer = new CustomerDto() { Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                    EndCustomer = new EndCustomerDto() { Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = new RoleDto() { Title = project.Role.Title, Notes = project.Role.Notes },
                    Sector = new SectorDto() { Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = new UserDto() { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }
                });
            }

            return projectListDto;
        }

        public async Task<IEnumerable<ProjectDto>> GetLatestProjectsAsync(int count)
        {
            var latestProjects = (await _projectRepository.GetLatestProjectsAsync(count))
                .OrderByDescending(p => p.StartDate)
                .Take(count)
                .ToList();

            var latestProjectsDto = new List<ProjectDto>();

            foreach (var project in latestProjects)
            {
                latestProjectsDto.Add(new ProjectDto
                {
                    Title = project.Title,
                    Customer = new CustomerDto {Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                    EndCustomer = new EndCustomerDto {Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = new RoleDto { Title = project.Role.Title, Notes = project.Role.Notes },
                    Sector = new SectorDto {Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = new UserDto { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }

                });
            }

            return latestProjectsDto;
        }


        public async Task<IEnumerable<CustomerGroupDto>> GetGroupedByCustomerAsync()
        {
            var groupedProject = await _projectRepository.GetGroupedByCustomerAsync();
            var result = new List<CustomerGroupDto>();
            foreach (var grouped in groupedProject)
            {
                var customerGroup = new CustomerGroupDto()
                {
                    Id = grouped.Key.Id,
                    Title = grouped.Key.Title,
                    Projects = grouped.Select(projectFilter => new ProjectDto
                    {
                        Id = projectFilter.Id,
                        Title = projectFilter.Title,
                        Customer = new CustomerDto { Id = projectFilter.Customer.Id, Title = projectFilter.Customer.Title, Notes = projectFilter.Customer.Notes },
                        EndCustomer = new EndCustomerDto { Id = projectFilter.EndCustomer.Id, Title = projectFilter.EndCustomer.Title, Notes = projectFilter.EndCustomer.Notes },
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = new RoleDto() { Title = projectFilter.Role.Title, Notes = projectFilter.Role.Notes },
                        Sector = new SectorDto() {Id = projectFilter.Sector.Id, Title = projectFilter.Sector.Title, Description = projectFilter.Sector.Description },
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = new UserDto() { DisplayName = projectFilter.User.DisplayName, FirstName = projectFilter.User.FirstName, LastName = projectFilter.User.LastName, UserName = projectFilter.User.UserName },
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = new BusinessUnitDto() { Id = projectFilter.BusinessUnit.Id, Title = projectFilter.BusinessUnit.Title }
                    }).ToList()
                };
                result.Add(customerGroup);
            }
            return result;
        }

        public async Task<IEnumerable<SectorGroupDto>> GetGroupedBySectorAsync()
        {
            var groupedSector = await _projectRepository.GetGroupedBySectorAsync();
            var result = new List<SectorGroupDto>();
            foreach (var sector in groupedSector)
            {
                var sectorGroup = new SectorGroupDto()
                {
                    Id = sector.Key.Id,
                    Title = sector.Key.Title,
                    Projects = sector.Select(projectFilter => new ProjectDto
                    {
                        Id = projectFilter.Id,
                        Title = projectFilter.Title,
                        Customer = new CustomerDto { Id = projectFilter.Customer.Id, Title = projectFilter.Customer.Title, Notes = projectFilter.Customer.Notes },
                        EndCustomer = new EndCustomerDto { Id = projectFilter.EndCustomer.Id, Title = projectFilter.EndCustomer.Title, Notes = projectFilter.EndCustomer.Notes },
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = new RoleDto() { Title = projectFilter.Role.Title, Notes = projectFilter.Role.Notes },
                        Sector = new SectorDto() {Id = projectFilter.Sector.Id, Title = projectFilter.Sector.Title, Description = projectFilter.Sector.Description },
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = new UserDto() { DisplayName = projectFilter.User.DisplayName, FirstName = projectFilter.User.FirstName, LastName = projectFilter.User.LastName, UserName = projectFilter.User.UserName },
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = new BusinessUnitDto() { Id = projectFilter.BusinessUnit.Id, Title = projectFilter.BusinessUnit.Title }
                    }).ToList()
                };
                result.Add(sectorGroup);
            }
            return result;
        }

        public async Task<IEnumerable<RoleGroupDto>> GetGroupedByRoleAsync()
        {
            var groupedRole = await _projectRepository.GetGroupedByRoleAsync();
            var result = new List<RoleGroupDto>();
            foreach (var role in groupedRole)
            {
                    var roleGroupDto = new RoleGroupDto()
                    {
                        Id = role.Key.Id,
                        Title = role.Key.Title,
                        Projects = role.Select(projectFilter => new ProjectDto
                        {
                            Id = projectFilter.Id,
                            Title = projectFilter.Title,
                            Customer = new CustomerDto { Id = projectFilter.Customer.Id, Title = projectFilter.Customer.Title, Notes = projectFilter.Customer.Notes },
                            EndCustomer = new EndCustomerDto { Id = projectFilter.EndCustomer.Id, Title = projectFilter.EndCustomer.Title, Notes = projectFilter.EndCustomer.Notes },
                            StartDate = projectFilter.StartDate,
                            EndDate = projectFilter.EndDate,
                            Description = projectFilter.Description,
                            WorkOrder = projectFilter.WorkOrder,
                            Role = new RoleDto() { Title = projectFilter.Role.Title, Notes = projectFilter.Role.Notes },
                            Sector = new SectorDto() {Id = projectFilter.Sector.Id, Title = projectFilter.Sector.Title, Description = projectFilter.Sector.Description },
                            Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                            JobCode = projectFilter.JobCode,
                            User = new UserDto() { DisplayName = projectFilter.User.DisplayName, FirstName = projectFilter.User.FirstName, LastName = projectFilter.User.LastName, UserName = projectFilter.User.UserName },
                            Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                            Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                            Goals = projectFilter.Goals,
                            Repository = projectFilter.Repository,
                            GoalSolutions = projectFilter.GoalSolutions,
                            SolutionsImpact = projectFilter.SolutionsImpact,
                            BusinessUnit = new BusinessUnitDto() {Id = projectFilter.BusinessUnit.Id, Title = projectFilter.BusinessUnit.Title }
                        }).ToList()
                    };
                    result.Add(roleGroupDto);
                
            }

            return result;
        }

        public async Task<IEnumerable<EndCustomerGroupDto>> GetGroupedByEndCustomerAsync()
        {
            var groupedEndCustomer = await _projectRepository.GetGroupedByEndCustomerAsync();
            var result = new List<EndCustomerGroupDto>();

            foreach (var endCustomer in groupedEndCustomer)
            {
                var endCustomerDto = new EndCustomerGroupDto()
                {
                    Id = endCustomer.Key.Id,
                    Title = endCustomer.Key.Title,
                    Projects = endCustomer.Select(projectFilter => new ProjectDto
                    {
                        Id = projectFilter.Id,
                        Title = projectFilter.Title,
                        Customer = new CustomerDto { Id = projectFilter.Customer.Id, Title = projectFilter.Customer.Title, Notes = projectFilter.Customer.Notes },
                        EndCustomer = new EndCustomerDto { Id = projectFilter.EndCustomer.Id, Title = projectFilter.EndCustomer.Title, Notes = projectFilter.EndCustomer.Notes },
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = new RoleDto() { Title = projectFilter.Role.Title, Notes = projectFilter.Role.Notes },
                        Sector = new SectorDto() {Id = projectFilter.Sector.Id, Title = projectFilter.Sector.Title, Description = projectFilter.Sector.Description },
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = new UserDto() { DisplayName = projectFilter.User.DisplayName, FirstName = projectFilter.User.FirstName, LastName = projectFilter.User.LastName, UserName = projectFilter.User.UserName },
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = new BusinessUnitDto() {Id = projectFilter.BusinessUnit.Id, Title = projectFilter.BusinessUnit.Title }
                    }).ToList()
                };
                result.Add(endCustomerDto);
            }

            return result;
        }

        public async Task<IEnumerable<GroupByDateDto>> GetGroupedByStartDateAsync()
        {
            var groupedProjects = await _projectRepository.GetGroupedByStartDateAsync();
            var result = groupedProjects.Select(group => new GroupByDateDto
            {
                Date = group.Key,
                Projects = group.Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Title = project.Title,
                    Customer = new CustomerDto { Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                    EndCustomer = new EndCustomerDto { Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = new RoleDto() { Title = project.Role.Title, Notes = project.Role.Notes },
                    Sector = new SectorDto() {Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = new UserDto() { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }
                }).ToList()
            });

            return result;
        }


        public async Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectRequestDto updateProjectRequestDto)
        {
            await UpdateProjectAsync(projectId, updateProjectRequestDto);
            return null;
        }

        public async Task<IEnumerable<ProjectDto>> GetByInProgressStatusAsync()
        {
            var projectList = await _projectRepository.GetByInProgressStatusAsync();

            var projectListDto = new List<ProjectDto>();

            foreach (var project in projectList) {
                projectListDto.Add(new ProjectDto()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Customer = new CustomerDto { Id = project.Customer.Id, Title = project.Customer.Title, Notes = project.Customer.Notes },
                    EndCustomer = new EndCustomerDto { Id = project.EndCustomer.Id, Title = project.EndCustomer.Title, Notes = project.EndCustomer.Notes },
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder, 
                    Role = new RoleDto() { Title = project.Role.Title, Notes = project.Role.Notes },
                    Sector = new SectorDto() {Id = project.Sector.Id, Title = project.Sector.Title, Description = project.Sector.Description },
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = new UserDto() { DisplayName = project.User.DisplayName, FirstName = project.User.FirstName, LastName = project.User.LastName, UserName = project.User.UserName },
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = new BusinessUnitDto() { Id = project.BusinessUnit.Id, Title = project.BusinessUnit.Title }
                });
            }

            return projectListDto;
        }
    }
}