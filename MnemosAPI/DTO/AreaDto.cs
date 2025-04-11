using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class AreaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SkillDto> Skill { get; set; }
    }
}
