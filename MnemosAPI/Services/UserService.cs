using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;
using System.Collections.Generic;

namespace MnemosAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(userList);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }
    
    }
}
