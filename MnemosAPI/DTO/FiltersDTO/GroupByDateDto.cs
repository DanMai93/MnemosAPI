namespace MnemosAPI.DTO.FiltersDTO
{
    public class GroupByDateDto
    {
        public DateOnly? Date { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
