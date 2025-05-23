using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        Task<IEnumerable<GroupByDateDto>> GetGroupedByStartDateAsync();

        Task<IEnumerable<ProjectDto>> GetByInProgressStatusAsync();

        Task<IEnumerable<ProjectDto>> GetLatestProjectsAsync(int count);

        Task<ProjectDto> GetProjectAsync(int projectId);

        Task<ProjectDto> UpdateProjectAsync(int projectId, JsonPatchDocument<UpdateProjectRequestDto> updateProjectRequestDto, ModelStateDictionary ModelState);

        Task<IEnumerable<ProjectDto>> GetByInputStringAsync(string inputString);
        
    }   
}
