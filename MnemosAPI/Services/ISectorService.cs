using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDto>> GetSectorsAsync();

        Task<SectorDto> GetSectorAsync(int sectorId);
    }
}
