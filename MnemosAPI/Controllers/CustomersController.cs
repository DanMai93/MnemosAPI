using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            return await customerService.GetCustomersAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync([FromRoute]int id)
        {
            var customer = await customerService.GetCustomerAsync(id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

    }
}
