using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftSkillController : ControllerBase
    {
        private readonly ISoftSkillService softSkillService;
        private readonly IMapper mapper;

        public SoftSkillController(ISoftSkillService softSkillService, IMapper mapper)
        {
            this.softSkillService = softSkillService;
            this.mapper = mapper;
        }

        // GET: api/SoftSkill
        [HttpGet]
        public async Task<IEnumerable<SoftSkillDto>> GetSoftSkillsAsync()
        {
            return await softSkillService.GetSoftSkillsAsync();
        }

    }
}
