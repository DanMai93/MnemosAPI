using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetSkillsAsync();

        Task<SkillDto> GetSkillAsync(int skillId);
    }
}
