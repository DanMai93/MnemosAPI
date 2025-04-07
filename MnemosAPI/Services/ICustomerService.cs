using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();

        Task<CustomerDto> GetCustomerAsync(int customerId);
    }
}
