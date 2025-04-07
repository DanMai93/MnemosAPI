using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Models;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
        {
            var skillList = await _skillRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skillList);
        }

        public async Task<SkillDto> GetSkillAsync(int skillId)
        {
            var skill = await _skillRepository.GetByIdAsync(skillId);
            return _mapper.Map<SkillDto>(skill);
        }
    }
}
