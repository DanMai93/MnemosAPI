using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var customerList = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customerList);
        }
    }
}
