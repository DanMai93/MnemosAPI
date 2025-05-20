using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementToolController : ControllerBase
    {
        private readonly IManagementToolService managementToolService;
        private readonly IMapper mapper;

        public ManagementToolController(IManagementToolService managementToolService, IMapper mapper)
        {
            this.managementToolService = managementToolService;
            this.mapper = mapper;
        }

        // GET: api/ManagementTool
        [HttpGet]
        public async Task<IEnumerable<ManagementToolDto>> GetManagementToolsAsync()
        {
            return await managementToolService.GetManagementToolsAsync();
        }

    }
}
