using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
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
                EndCustomer = addProjectRequestDto.EndCustomer,
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
                EndCustomer = project.EndCustomer,
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

        public async Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectRequestDto updateProjectRequestDto)
        {
            await UpdateProjectAsync(projectId, updateProjectRequestDto);
            return null;
        }
    }
}
