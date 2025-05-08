using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IManagementToolService
    {
        Task<IEnumerable<ManagementToolDto>> GetManagementToolsAsync();

        Task<ManagementToolDto> GetManagementToolAsync(int id);
    }
}
