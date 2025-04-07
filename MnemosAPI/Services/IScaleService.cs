using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IScaleService
    {
        Task<ScaleDto> GetScaleAsync(int scaleId);

        Task<IEnumerable<ScaleDto>> GetScalesAsync();
    }
}
