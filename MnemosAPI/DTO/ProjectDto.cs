using MnemosAPI.Models;
using MnemosAPI.Utilities;
namespace MnemosAPI.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public CustomerDto? Customer { get; set; }

        public EndCustomerDto? EndCustomer { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Description { get; set; }

        public string? WorkOrder { get; set; }

        public List<SkillDto>? Skills { get; set; }

        public List<ArchitectureDto>? Architectures { get; set; }

        public List<ManagementToolDto>? ManagementTools { get; set; }

        public List<WorkMethodDto>? WorkMethods { get; set; }

        public List<SoftSkillDto>? SoftSkills { get; set; }

        public RoleDto? Role { get; set; }

        public SectorDto? Sector { get; set; }

        public string? JobCode { get; set; }

        public UserDto? User { get; set; }

        public DifficultiesEnum? Difficulty { get; set; }

        public StatusesEnum? Status { get; set; }

        public string? Goals { get; set; }

        public string? Repository { get; set; }

        public string? GoalSolutions { get; set; }

        public string? SolutionsImpact { get; set; }

        public BusinessUnitDto? BusinessUnit { get; set; }



    }
}
