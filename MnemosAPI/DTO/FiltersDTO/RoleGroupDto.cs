namespace MnemosAPI.DTO.FiltersDTO
{
    public class RoleGroupDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<ProjectDto> Projects { get; set; }
    }
}
