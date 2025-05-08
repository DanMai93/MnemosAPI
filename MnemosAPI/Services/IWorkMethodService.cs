using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IWorkMethodService
    {
        Task<IEnumerable<WorkMethodDto>> GetWorkMethodsAsync();
    }

}
