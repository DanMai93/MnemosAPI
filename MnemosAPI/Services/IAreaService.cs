using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAreasAsync();

        Task<AreaDto> GetAreaByIdAsync(int areaId);
    }
}
