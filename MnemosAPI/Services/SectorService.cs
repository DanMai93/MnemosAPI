using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectorDto>> GetSectorsAsync()
        {
            var sectorList = await _sectorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SectorDto>>(sectorList);
        }

        public async Task<SectorDto> GetSectorAsync(int sectorId)
        {
            var sector = await _sectorRepository.GetByIdAsync(sectorId);
            return _mapper.Map<SectorDto>(sector);
        }
    }
}
