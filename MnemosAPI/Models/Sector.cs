﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
namespace MnemosAPI.Models;

public partial class Sector
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}