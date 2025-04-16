using MnemosAPI.Utilities;

namespace MnemosAPI.DTO.FiltersDTO
{
    public class ProjectFilterDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int CustomerId { get; set; }

        public string? EndCustomer { get; set; }

        public DateOnly StartDate { get; set; }

        public RoleDto? Role { get; set; }

        public int RoleId { get; set; }

        public SectorDto? Sector { get; set; }

        public int SectorId { get; set; }

        public UserDto User { get; set; }

        public int UserId { get; set; }

    }
        
}
