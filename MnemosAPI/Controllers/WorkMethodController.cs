using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkMethodController : ControllerBase
    {
        private readonly IWorkMethodService workMethodService;
        private readonly IMapper mapper;

        public WorkMethodController(IWorkMethodService workMethodService, IMapper mapper)
        {
            this.workMethodService = workMethodService;
            this.mapper = mapper;
        }

        // GET: api/WorkMethod
        [HttpGet]
        public async Task<IEnumerable<WorkMethodDto>> GetWorkMethodsAsync()
        {
            return await workMethodService.GetWorkMethodsAsync();
        }

    }
}
