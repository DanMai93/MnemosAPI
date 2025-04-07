using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO;
using MnemosAPI.DTO.UpdateRequestDto;

namespace MnemosAPI.Services
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(AddProjectRequestDto addProjectRequestDto);

        Task DeleteProjectAsync(int projectId);

        Task<IEnumerable<ProjectDto>> GetProjectsAsync();

        Task<ProjectDto> GetProjectAsync(int projectId);

        Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectRequestDto updateProjectRequestDto);
    }
}
