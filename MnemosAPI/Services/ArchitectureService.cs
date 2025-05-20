using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;
using MnemosAPI.Repository.SQLRepository;

namespace MnemosAPI.Services
{
    public class ArchitectureService : IArchitectureService
    {
        private readonly IArchitectureRepository _architectureRepository;
        private readonly IMapper _mapper;

        public ArchitectureService(IArchitectureRepository architectureRepository, IMapper mapper)
        {
            _architectureRepository = architectureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArchitectureDto>> GetArchitecturesAsync()
        {
            var architectureList = await _architectureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ArchitectureDto>>(architectureList);
        }
        public async Task<ArchitectureDto> GetArchitectureAsync(int id)
        {
            var architecture = await  _architectureRepository.GetByIdAsync(id);
            return _mapper.Map<ArchitectureDto>(architecture);
        }

    }
}
