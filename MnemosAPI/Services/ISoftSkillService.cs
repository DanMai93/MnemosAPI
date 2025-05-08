using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface ISoftSkillService
    {
        Task<IEnumerable<SoftSkillDto>> GetSoftSkillsAsync();
    }
}
