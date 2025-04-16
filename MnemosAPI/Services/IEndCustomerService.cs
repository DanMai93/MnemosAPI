using MnemosAPI.DTO;

namespace MnemosAPI.Services
{
    public interface IEndCustomerService
    {
        Task<IEnumerable<EndCustomerDto>> GetEndCustomersAsync();

        Task<EndCustomerDto> GetEndCustomerAsync(int endCustomerId);
    }
}
