using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndCustomerController : ControllerBase
    {
        private readonly IEndCustomerService endCustomerService;
        private readonly IMapper mapper;

        public EndCustomerController(IEndCustomerService endCustomerService, IMapper mapper)
        {
            this.endCustomerService = endCustomerService;
            this.mapper = mapper;
        }

        // GET: api/EndCustomers
        [HttpGet]
        public async Task<IEnumerable<EndCustomerDto>> GetEndCustomersAsync()
        {
            return await endCustomerService.GetEndCustomersAsync();
        }

        // GET: api/EndCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndCustomerDto>> GetEndCustomerByIdAsync([FromRoute]int id)
        {
            var endCustomer = await endCustomerService.GetEndCustomerAsync(id);

            if(endCustomer == null)
            {
                return NotFound();
            }

            return Ok(endCustomer);
        }
    }
}
