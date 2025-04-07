using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetRoleAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            return _mapper.Map<RoleDto>(role);
        }
    }
}
