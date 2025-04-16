namespace MnemosAPI.DTO
{
    public class EndCustomerDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Notes { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
