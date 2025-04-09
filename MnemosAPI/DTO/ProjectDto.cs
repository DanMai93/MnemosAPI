using MnemosAPI.Models;
using MnemosAPI.Utilities;
namespace MnemosAPI.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string? EndCustomer { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Description { get; set; }

        public string? WorkOrder { get; set; }

        public List<Skill> Skills { get; set; }

        public int SkillId { get; set; }

        public Role? Role { get; set; }

        public int RoleId { get; set; }

        public Sector? Sector { get; set; }

        public int SectorId { get; set; }

        public string? JobCode { get; set; }

        public User? User { get; set; }

        public DifficultiesEnum? Difficulty { get; set; }

        public StatusesEnum? Status { get; set; }

        public string? Goals { get; set; }
    }
}
