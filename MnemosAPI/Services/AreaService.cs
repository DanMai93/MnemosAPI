using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository areaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<AreaDto> GetAreaByIdAsync(int areaId)
        {
            var area = await _areaRepository.GetByIdAsync(areaId);
            return _mapper.Map<AreaDto>(area);
        }

        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            return _mapper.Map<IEnumerable<AreaDto>>(await _areaRepository.GetAllAsync());
        }
    }
}
