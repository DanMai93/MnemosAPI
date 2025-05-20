using MnemosAPI.Models;
using MnemosAPI.Utilities;
namespace MnemosAPI.DTO.UpdateRequestDto
{
    public class UpdateProjectRequestDto
    {
        public string Title { get; set; }

        public int CustomerId { get; set; }

        public int EndCustomerId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Description { get; set; }

        public string? WorkOrder { get; set; }

        public int RoleId { get; set; }

        public int SectorId { get; set; }

        public int[] Skills { get; set; }

        public int[] Architectures { get; set; }

        public int[] WorkMethods { get; set; }

        public int[] ManagementTools { get; set; }

        public int[] SoftSkills { get; set; }

        public string? JobCode { get; set; }

        public int UserId { get; set; }

        public DifficultiesEnum Difficulty { get; set; }

        public StatusesEnum Status { get; set; }

        public string? Goals { get; set; }

        public string Repository { get; set; }

        public string GoalSolutions { get; set; }

        public string SolutionsImpact { get; set; }

        public int? BusinessUnitId { get; set; }
    }
}
