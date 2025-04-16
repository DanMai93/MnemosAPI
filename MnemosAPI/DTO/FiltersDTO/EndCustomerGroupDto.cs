namespace MnemosAPI.DTO.FiltersDTO
{
    public class EndCustomerGroupDto
    {
        public int Id { get; set; }
        public string Title { get; internal set; }
        
        public List<ProjectFilterDto> Projects { get; set; }
        
    }
}
