using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService areaService;
        private readonly IMapper mapper;

        public AreaController(IAreaService areaService, IMapper mapper)
        {
            this.areaService = areaService;
            this.mapper = mapper;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            return await areaService.GetAreasAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDto>> GetAreaByIdAsync([FromRoute] int id)
        {
            var area = await areaService.GetAreaByIdAsync(id);

            if(area == null)
            {
                return NotFound();
            }

            return area;
        }
    }
}
