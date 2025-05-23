﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.Services;

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

        // GET: api/Projects/5
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
