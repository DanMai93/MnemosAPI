using AutoMapper;
using MnemosAPI.DTO;
using MnemosAPI.Repository;

namespace MnemosAPI.Services
{
    public class WorkMethodService : IWorkMethodService
    {
        private readonly IWorkMethodRepository _workMethodRepository;
        private readonly IMapper _mapper;

        public WorkMethodService(IWorkMethodRepository workMethodRepository, IMapper mapper)
        {
            _workMethodRepository = workMethodRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkMethodDto>> GetWorkMethodsAsync()
        {
            var workMethodList = await _workMethodRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkMethodDto>>(workMethodList);
        }
    }
}
