namespace MnemosAPI.DTO.FiltersDTO
{
    public class CustomerGroupDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<ProjectFilterDto> Projects { get; set; }

        
    }
}
