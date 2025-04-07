using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<UserDto> GetUserByIdAsync(int userId);
    }
}
