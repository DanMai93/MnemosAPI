﻿namespace MnemosAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;
    }
}
