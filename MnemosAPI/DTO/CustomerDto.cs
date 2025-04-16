using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Notes { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
