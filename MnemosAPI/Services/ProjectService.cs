using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.Models;
using MnemosAPI.Repository;
using MnemosAPI.Utilities;

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
                Goals = addProjectRequestDto.Goals
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

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                CustomerId = project.CustomerId,
                EndCustomerId = project.EndCustomerId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                WorkOrder = project.WorkOrder,
                RoleId = (int)project.RoleId!,
                Skills = _mapper.Map<List<SkillDto>>(project.Skills),
                Sector = _mapper.Map<SectorDto>(project.Sector),
                JobCode = project.JobCode,
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Status = Enum.Parse<StatusesEnum>(project.Status),
                Goals = project.Goals,
            };

            return projectDto;
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteByIdAsync(projectId);

        }

        public async Task<ProjectDto?> GetProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync()
        {
            var projectList = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projectList);
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
                    Projects = grouped.Select(projectFilter => new ProjectFilterDto
                    {
                        Id = grouped.Key.Id,
                        Title = grouped.Key.Title,
                        EndCustomerId = grouped.Key.Id,
                        StartDate = grouped.Key.Projects.First().StartDate,
                        RoleId = grouped.Key.Id,
                        SectorId = grouped.Key.Id,
                        UserId = grouped.Key.Id
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
                    Projects = sector.Select(projectFilter => new ProjectFilterDto
                    {
                        Id = sector.Key.Id,
                        Title = sector.Key.Title,
                        CustomerId = sector.Key.Id,
                        EndCustomerId = sector.Key.Id,
                        StartDate = sector.Key.Projects.First().StartDate,
                        RoleId = sector.Key.Id,
                        SectorId = sector.Key.Id,
                        UserId = sector.Key.Id
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
                        Projects = role.Select(projectFilter => new ProjectFilterDto
                        {
                            Id = role.Key.Id,
                            Title = role.Key.Title,
                            CustomerId = role.Key.Id,
                            EndCustomerId = role.Key.Id,
                            StartDate = role.Key.Projects.First().StartDate,
                            RoleId = role.Key.Id,
                            SectorId = role.Key.Id,
                            UserId = role.Key.Id
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
            foreach (var endCustomer  in groupedEndCustomer)
            {
                var endCustomerDto = new EndCustomerGroupDto()
                {
                    Id = endCustomer.Key.Id,
                    Title = endCustomer.Key.Title,
                    Projects = endCustomer.Select(projectFilter => new ProjectFilterDto
                    {
                        Id = endCustomer.Key.Id,
                        Title = endCustomer.Key.Title,
                        CustomerId = endCustomer.Key.Id,
                        EndCustomerId = endCustomer.Key.Id,
                        StartDate = endCustomer.Key.Projects?.FirstOrDefault()?.StartDate ?? default,
                        RoleId = endCustomer.Key.Id,
                        SectorId = endCustomer.Key.Id,
                        UserId = endCustomer.Key.Id

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
                    StartDate = project.StartDate,
                    EndDate = project.EndDate
                }).ToList()
            });

            return result;
        }


        public async Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectRequestDto updateProjectRequestDto)
        {
            await UpdateProjectAsync(projectId, updateProjectRequestDto);
            return null;
        }
    }
}
