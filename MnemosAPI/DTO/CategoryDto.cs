﻿using MnemosAPI.Models;

namespace MnemosAPI.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SkillDto> Skills { get; set; }
    }
}
