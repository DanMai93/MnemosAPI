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
        private readonly IArchitectureRepository _architectureRepository;
        private readonly IWorkMethodRepository _workMethodRepository;
        private readonly IManagementToolRepository _managementToolRepository;
        private readonly ISoftSkillRepository _softSkillRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, ISkillRepository skillRepository, IArchitectureRepository architectureRepository,
            IWorkMethodRepository workMethodRepository, IManagementToolRepository managementToolRepository, ISoftSkillRepository softSkillRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _skillRepository = skillRepository;
            _architectureRepository = architectureRepository;
            _workMethodRepository = workMethodRepository;
            _managementToolRepository = managementToolRepository;
            _softSkillRepository = softSkillRepository;
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
                else
                {
                    throw new ArgumentException("Skill non trovata con id " + skillId);
                }
            }

            foreach (var architectureId in addProjectRequestDto.Architectures)
            {
           
                var architecture = await _architectureRepository.GetByIdAsync(architectureId);
                if (architecture != null)
                {
                    project.Architectures.Add(architecture);
                }
                else
                {
                    throw new ArgumentException("Architecture non trovata con id " + architectureId);
                }
            }

            foreach (var workMethodId in addProjectRequestDto.WorkMethods)
            {
              
                var workMethod = await _workMethodRepository.GetByIdAsync(workMethodId);
                if (workMethod != null)
                {
                    project.WorkMethods.Add(workMethod);
                }
                else
                {
                    throw new ArgumentException("Work method non trovato con id " + workMethodId);
                }
            }


            foreach (var managementToolId in addProjectRequestDto.ManagementTools)
            {

                var managementTool = await _managementToolRepository.GetByIdAsync(managementToolId);
                if (managementTool != null)
                {
                    project.ManagementTools.Add(managementTool);
                }
                else
                {
                    throw new ArgumentException("Management tool non trovato con id " + managementToolId);
                }
            }

            foreach (var softSkillId in addProjectRequestDto.SoftSkills)
            {

                var softSkill = await _softSkillRepository.GetByIdAsync(softSkillId);
                if (softSkill != null)
                {
                    project.SoftSkills.Add(softSkill);
                } else
                {
                    throw new ArgumentException("Soft skill non trovata con id " + softSkillId);
                }
            }

            project = await _projectRepository.AddAsync(project);

            var addedProject = await _projectRepository.GetByIdWithForeignKeysAsync(project.Id);

            var projectDto = new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Customer = _mapper.Map<CustomerDto>(project.Customer),
                EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                WorkOrder = project.WorkOrder,
                Role = _mapper.Map<RoleDto>(project.Role),
                Sector = _mapper.Map<SectorDto>(project.Sector),
                Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                JobCode = project.JobCode,
                User = _mapper.Map<UserDto>(project.User),
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Status = Enum.Parse<StatusesEnum>(project.Status),
                Goals = project.Goals,
                Repository = project.Repository,
                GoalSolutions = project.GoalSolutions,
                SolutionsImpact = project.SolutionsImpact,
                BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)
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

            if (project == null)
            {
                return null;
            }

            var projectDto = new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Customer = _mapper.Map<CustomerDto>(project.Customer),
                EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                WorkOrder = project.WorkOrder,
                Role = _mapper.Map<RoleDto>(project.Role),
                Sector =  _mapper.Map<SectorDto>(project.Sector),
                Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                JobCode = project.JobCode,
                User = _mapper.Map<UserDto>(project.User),
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Status = Enum.Parse<StatusesEnum>(project.Status),
                Goals = project.Goals,
                Repository = project.Repository,
                GoalSolutions = project.GoalSolutions,
                SolutionsImpact = project.SolutionsImpact,
                BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)
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
                Customer = _mapper.Map<CustomerDto>(project.Customer),
                EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                WorkOrder = project.WorkOrder,
                Role = _mapper.Map<RoleDto>(project.Role),
                Sector =  _mapper.Map<SectorDto>(project.Sector),
                Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                JobCode = project.JobCode,
                User = _mapper.Map<UserDto>(project.User),
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Status = Enum.Parse<StatusesEnum>(project.Status),
                Goals = project.Goals,
                Repository = project.Repository,
                GoalSolutions = project.GoalSolutions,
                SolutionsImpact = project.SolutionsImpact,
                BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)
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
                    Id = project.Id,
                    Title = project.Title,
                    Customer = _mapper.Map<CustomerDto>(project.Customer),
                    EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = _mapper.Map<RoleDto>(project.Role),
                    Sector = _mapper.Map<SectorDto>(project.Sector),
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = _mapper.Map<UserDto>(project.User),
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                    Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                    WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                    ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                    SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)

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
                        Customer = _mapper.Map<CustomerDto>(projectFilter.Customer),
                        EndCustomer = _mapper.Map<EndCustomerDto>(projectFilter.EndCustomer),
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = _mapper.Map<RoleDto>(projectFilter.Role),
                        Sector = _mapper.Map<SectorDto>(projectFilter.Sector),
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = _mapper.Map<UserDto>(projectFilter.User),
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = _mapper.Map<BusinessUnitDto>(projectFilter.BusinessUnit),
                        Architectures = _mapper.Map<List<ArchitectureDto>>(projectFilter.Architectures),
                        WorkMethods = _mapper.Map<List<WorkMethodDto>>(projectFilter.WorkMethods),
                        ManagementTools = _mapper.Map<List<ManagementToolDto>>(projectFilter.ManagementTools),
                        SoftSkills = _mapper.Map<List<SoftSkillDto>>(projectFilter.SoftSkills)
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
                        Customer = _mapper.Map<CustomerDto>(projectFilter.Customer),
                        EndCustomer = _mapper.Map<EndCustomerDto>(projectFilter.EndCustomer),
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = _mapper.Map<RoleDto>(projectFilter.Role),
                        Sector = _mapper.Map<SectorDto>(projectFilter.Sector),
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = _mapper.Map<UserDto>(projectFilter.User),
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = _mapper.Map<BusinessUnitDto>(projectFilter.BusinessUnit),
                        Architectures = _mapper.Map<List<ArchitectureDto>>(projectFilter.Architectures),
                        WorkMethods = _mapper.Map<List<WorkMethodDto>>(projectFilter.WorkMethods),
                        ManagementTools = _mapper.Map<List<ManagementToolDto>>(projectFilter.ManagementTools),
                        SoftSkills = _mapper.Map<List<SoftSkillDto>>(projectFilter.SoftSkills)
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
                        Customer = _mapper.Map<CustomerDto>(projectFilter.Customer),
                        EndCustomer = _mapper.Map<EndCustomerDto>(projectFilter.EndCustomer),
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = _mapper.Map<RoleDto>(projectFilter.Role),
                        Sector = _mapper.Map<SectorDto>(projectFilter.Sector),
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = _mapper.Map<UserDto>(projectFilter.User),
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = _mapper.Map<BusinessUnitDto>(projectFilter.BusinessUnit),
                        Architectures = _mapper.Map<List<ArchitectureDto>>(projectFilter.Architectures),
                        WorkMethods = _mapper.Map<List<WorkMethodDto>>(projectFilter.WorkMethods),
                        ManagementTools = _mapper.Map<List<ManagementToolDto>>(projectFilter.ManagementTools),
                        SoftSkills = _mapper.Map<List<SoftSkillDto>>(projectFilter.SoftSkills)
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
                        Customer = _mapper.Map<CustomerDto>(projectFilter.Customer),
                        EndCustomer = _mapper.Map<EndCustomerDto>(projectFilter.EndCustomer),
                        StartDate = projectFilter.StartDate,
                        EndDate = projectFilter.EndDate,
                        Description = projectFilter.Description,
                        WorkOrder = projectFilter.WorkOrder,
                        Role = _mapper.Map<RoleDto>(projectFilter.Role),
                        Sector = _mapper.Map<SectorDto>(projectFilter.Sector),
                        Skills = _mapper.Map<List<SkillDto>>(projectFilter.Skills),
                        JobCode = projectFilter.JobCode,
                        User = _mapper.Map<UserDto>(projectFilter.User),
                        Difficulty = Enum.Parse<DifficultiesEnum>(projectFilter.Difficulty),
                        Status = Enum.Parse<StatusesEnum>(projectFilter.Status),
                        Goals = projectFilter.Goals,
                        Repository = projectFilter.Repository,
                        GoalSolutions = projectFilter.GoalSolutions,
                        SolutionsImpact = projectFilter.SolutionsImpact,
                        BusinessUnit = _mapper.Map<BusinessUnitDto>(projectFilter.BusinessUnit),
                        Architectures = _mapper.Map<List<ArchitectureDto>>(projectFilter.Architectures),
                        WorkMethods = _mapper.Map<List<WorkMethodDto>>(projectFilter.WorkMethods),
                        ManagementTools = _mapper.Map<List<ManagementToolDto>>(projectFilter.ManagementTools),
                        SoftSkills = _mapper.Map<List<SoftSkillDto>>(projectFilter.SoftSkills)
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
                    Customer = _mapper.Map<CustomerDto>(project.Customer),
                    EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = _mapper.Map<RoleDto>(project.Role),
                    Sector = _mapper.Map<SectorDto>(project.Sector),
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = _mapper.Map<UserDto>(project.User),
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                    Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                    WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                    ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                    SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)
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

            foreach (var project in projectList)
            {
                projectListDto.Add(new ProjectDto()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Customer = _mapper.Map<CustomerDto>(project.Customer),
                    EndCustomer = _mapper.Map<EndCustomerDto>(project.EndCustomer),
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    WorkOrder = project.WorkOrder,
                    Role = _mapper.Map<RoleDto>(project.Role),
                    Sector = _mapper.Map<SectorDto>(project.Sector),
                    Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                    JobCode = project.JobCode,
                    User = _mapper.Map<UserDto>(project.User),
                    Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                    Status = Enum.Parse<StatusesEnum>(project.Status),
                    Goals = project.Goals,
                    Repository = project.Repository,
                    GoalSolutions = project.GoalSolutions,
                    SolutionsImpact = project.SolutionsImpact,
                    BusinessUnit = _mapper.Map<BusinessUnitDto>(project.BusinessUnit),
                    Architectures = _mapper.Map<List<ArchitectureDto>>(project.Architectures),
                    WorkMethods = _mapper.Map<List<WorkMethodDto>>(project.WorkMethods),
                    ManagementTools = _mapper.Map<List<ManagementToolDto>>(project.ManagementTools),
                    SoftSkills = _mapper.Map<List<SoftSkillDto>>(project.SoftSkills)
                });
            }

            return projectListDto;
        }
    }
}