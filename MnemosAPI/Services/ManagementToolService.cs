using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class ManagementToolService : IManagementToolService
    {
        private readonly IManagementToolRepository _managementToolRepository;
        private readonly IMapper _mapper;

        public ManagementToolService(IManagementToolRepository managementToolRepository, IMapper mapper)
        {
            _managementToolRepository = managementToolRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManagementToolDto>> GetManagementToolsAsync()
        {
            var managementToolList = await _managementToolRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ManagementToolDto>>(managementToolList);
        }
    }
}
