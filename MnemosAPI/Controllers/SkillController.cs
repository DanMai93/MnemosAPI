using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService skillService;
        private readonly IMapper mapper;

        public SkillController(ISkillService skillService, IMapper mapper)
        {
            this.skillService = skillService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
        {
            return await skillService.GetSkillsAsync();
        }

        [HttpGet("{id}")]
        public async Task<SkillDto> GetSkillAsync(int id)
        {
            var skill = await skillService.GetSkillAsync(id);

            return skill;
        }
    }
}
