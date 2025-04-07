using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleController : ControllerBase
    {
        private readonly IScaleService scaleService;
        private readonly IMapper mapper;

        public ScaleController(IScaleService scaleService, IMapper mapper)
        {
            this.scaleService = scaleService;
            this.mapper = mapper;
        }
        [HttpGet]
        
        public async Task<IEnumerable<ScaleDto>> GetScalesAsync()
        {
            return await scaleService.GetScalesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ScaleDto> GetScaleAsync(int id)
        {
            var scale = await scaleService.GetScaleAsync(id);
            if (scale == null)
            {
                return null;
            }
            return scale;
        }
    }
}
