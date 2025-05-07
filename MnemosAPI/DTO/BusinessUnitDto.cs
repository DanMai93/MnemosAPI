namespace MnemosAPI.DTO
{
    public class BusinessUnitDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<ProjectDto> Projects { get; set; }
    }
}
