using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class ScaleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SkillDto> Skills { get; set; }
    }
}
