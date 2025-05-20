using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchitectureController : ControllerBase
    {
        private readonly IArchitectureService architectureService;
        private readonly IMapper mapper;

        public ArchitectureController(IArchitectureService architectureService, IMapper mapper)
        {
            this.architectureService = architectureService;
            this.mapper = mapper;
        }

        // GET: api/Architectures
        [HttpGet]
        public async Task<IEnumerable<ArchitectureDto>> GetArchitecturesAsync()
        {
            return await architectureService.GetArchitecturesAsync();
        }

    }
}
