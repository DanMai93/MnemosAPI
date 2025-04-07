using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.Models;
using MnemosAPI.Repository;
using MnemosAPI.Utilities;
using System.Collections.Generic;

namespace MnemosAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
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
                Goals = addProjectRequestDto.Goals
            };

            foreach (var item in addProjectRequestDto.Skills)
            {
                var skill = new Skill();
                skill.Id = item;
                project.Skills.Add(skill);
            }

            project = await _projectRepository.AddAsync(project);

#pragma warning disable CS8629 // Il tipo valore nullable non può essere Null.
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
                RoleId = (int)project.RoleId,
                Skills = (List<Skill>)project.Skills,
                Sector = project.Sector,
                JobCode = project.JobCode,
                Difficulty = Enum.Parse<DifficultiesEnum>(project.Difficulty),
                Goals = project.Goals,

            };
#pragma warning restore CS8629 // Il tipo valore nullable non può essere Null.

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
