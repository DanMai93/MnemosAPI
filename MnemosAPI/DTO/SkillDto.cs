using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class SkillDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category[] Categories { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public Area[] Areas { get; set; }

        public Scale[] Scales { get; set; }

        public Project[] Projects { get; set; }

    }
}
