using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService sectorService;
        private readonly IMapper mapper;

        public SectorController(ISectorService sectorService, IMapper mapper)
        {
            this.sectorService = sectorService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SectorDto>> GetSectorsAsync()
        {
            return await sectorService.GetSectorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<SectorDto> GetSectorAsync(int id)
        {
            var sector = await sectorService.GetSectorAsync(id);
            return sector;
        }
    }
}
