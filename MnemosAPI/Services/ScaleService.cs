using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IMapper _mapper;

        public ScaleService(IScaleRepository scaleRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        public async Task<ScaleDto> GetScaleAsync(int scaleId)
        {
            var scale = await _scaleRepository.GetByIdAsync(scaleId);
            return _mapper.Map<ScaleDto>(scale);
        }

        public async Task<IEnumerable<ScaleDto>> GetScalesAsync()
        {
            var scaleList = await _scaleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ScaleDto>>(scaleList);
        }
    }
}
