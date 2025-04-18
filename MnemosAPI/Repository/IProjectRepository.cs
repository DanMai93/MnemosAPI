using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.Models;
using MnemosAPI.Services;

namespace MnemosAPI.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<IGrouping<Customer, Project>>> GetGroupedByCustomerAsync();
        Task<List<IGrouping<Role, Project>>> GetGroupedByRoleAsync();
        Task<List<IGrouping<Sector, Project>>> GetGroupedBySectorAsync();
        Task<List<IGrouping<EndCustomer, Project>>> GetGroupedByEndCustomerAsync();
        Task<IEnumerable<Project>> GetLatestProjectsAsync(int count);
        Task<List<IGrouping<DateOnly, Project>>> GetGroupedByStartDateAsync();
        Task<List<Project>> GetAllWithForeignKeysAsync();
        Task<Project> GetAllWithForeignKeysByIdAsync(int id);
        Task<List<Project>> GetByInProgressStatusAsync();

    }

}
