using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class EndCustomerService : IEndCustomerService
    {
        private readonly IEndCustomerRepository _endCustomerRepository;
        private readonly IMapper _mapper;
        
        public EndCustomerService(IEndCustomerRepository endCustomerRepository, IMapper mapper)
        {
            _endCustomerRepository = endCustomerRepository;
            _mapper = mapper;
        }

        public async Task<EndCustomerDto> GetEndCustomerAsync(int endCustomerId)
        {
            var endCustomer = await _endCustomerRepository.GetByIdAsync(endCustomerId);
            return _mapper.Map<EndCustomerDto>(endCustomer);
        }

        public async Task<IEnumerable<EndCustomerDto>> GetEndCustomersAsync()
        {
            var endCustomerList = await _endCustomerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EndCustomerDto>>(endCustomerList);
        }
    }
}
