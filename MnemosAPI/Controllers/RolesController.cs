using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MnemosAPI.DTO;
using MnemosAPI.Services;

namespace MnemosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            return await roleService.GetRolesAsync();
        }

        [HttpGet("{id}")]
        public async Task<RoleDto> GetRoleAsync(int id)
        {
            var role = await roleService.GetRoleAsync(id);
            return role;
        }
    }

}