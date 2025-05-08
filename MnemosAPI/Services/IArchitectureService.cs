using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IArchitectureService
    {
        Task<IEnumerable<ArchitectureDto>> GetArchitecturesAsync();
    }
}
