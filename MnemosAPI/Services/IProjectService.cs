using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.DTO.UpdateRequestDto;

namespace MnemosAPI.Services
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(AddProjectRequestDto addProjectRequestDto);

        Task DeleteProjectAsync(int projectId);

        Task<IEnumerable<ProjectDto>> GetProjectsAsync();

        Task<IEnumerable<CustomerGroupDto>> GetGroupedByCustomerAsync();

        Task<IEnumerable<SectorGroupDto>> GetGroupedBySectorAsync();

        Task<IEnumerable<RoleGroupDto>> GetGroupedByRoleAsync();

        Task<IEnumerable<EndCustomerGroupDto>> GetGroupedByEndCustomerAsync();

        Task<ProjectDto> GetProjectAsync(int projectId);

        Task<ProjectDto> UpdateProjectAsync(int projectId, UpdateProjectRequestDto updateProjectRequestDto);
        
    }   
}
