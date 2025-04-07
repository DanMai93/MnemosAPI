using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync();

        Task<RoleDto> GetRoleAsync(int roleId);
    }
}
