using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class SoftSkillService : ISoftSkillService
    {
        private readonly ISoftSkillRepository _softSkillRepository;
        private readonly IMapper _mapper;

        public SoftSkillService(ISoftSkillRepository softSkillRepository, IMapper mapper)
        {
            _softSkillRepository = softSkillRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SoftSkillDto>> GetSoftSkillsAsync()
        {
            var softSkillsList = await _softSkillRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SoftSkillDto>>(softSkillsList);
        }
    }
}
