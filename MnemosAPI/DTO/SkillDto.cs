using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class SkillDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CategoryDto> Categories { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public List<AreaDto> Areas { get; set; }

        public List<ScaleDto> Scales { get; set; }

    }
}
