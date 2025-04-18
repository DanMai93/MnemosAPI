using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.FiltersDTO;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.Services;
using System.Linq;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IProjectService projectService;

        public ProjectsController(IMapper mapper, IProjectService projectService)
        {
            this.mapper = mapper;
            this.projectService = projectService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetProjectsAsync()
        {
            return await projectService.GetProjectsAsync();
        }

        [HttpGet("ByCustomer")]
        public async Task<ActionResult<IEnumerable<CustomerGroupDto>>> GetGroupedByCustomerAsync()
        {
            var result = await projectService.GetGroupedByCustomerAsync();
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);
        }

        [HttpGet("BySector")]
        public async Task<ActionResult<IEnumerable<SectorGroupDto>>> GetGroupedBySectorAsync()
        {
            var result = await projectService.GetGroupedBySectorAsync();
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);
            
        }

        [HttpGet("ByRole")]
        public async Task<ActionResult<IEnumerable<RoleGroupDto>>> GetGroupedByRoleAsync()
        {
            var result = await projectService.GetGroupedByRoleAsync();
            if(result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);
        }

        [HttpGet("ByEndCustomer")]
        public async Task<ActionResult<IEnumerable<EndCustomerGroupDto>>> GetGroupedByEndCustomerAsync()
        {
            var result = await projectService.GetGroupedByEndCustomerAsync();
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result); ;
        }

        [HttpGet("ByStartDate")]
        public async Task<ActionResult<IEnumerable<GroupByDateDto>>> GetGroupedByStartDateAsync()
        {
           var result = await projectService.GetGroupedByStartDateAsync();
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);

        }

        [HttpGet("Latest")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetLatestProjectsAsync([FromQuery] int count = 5)
        {
            var result = await projectService.GetLatestProjectsAsync(count);
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);
        }

        [HttpGet("ByActiveStatus")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetByActiveStatus()
        {
            var result = await projectService.GetByInProgressStatusAsync();
            if (result is null || result.Count() == 0)
            {
                return NotFound("Nessun ruolo trovato");
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectByIdAsync([FromRoute] int id)
        {
            var project = await projectService.GetProjectAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {

            var projectDto = await projectService.UpdateProjectAsync(id, updateProjectRequestDto);

            if (projectDto == null)
            {
                return NotFound();
            }

            return Ok(projectDto);
        }


        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] AddProjectRequestDto addProjectDto)
        {

            var projectDto = await projectService.CreateProjectAsync(addProjectDto);

            return Ok(projectDto);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProject([FromRoute] int id)
        {
            return Ok();
        }
    }
}

