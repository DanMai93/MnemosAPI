﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
namespace MnemosAPI.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    public string EndCustomer { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Description { get; set; }

    public int? RoleId { get; set; }

    public virtual Role Role { get; set; }

    public int? SectorId { get; set; }

    public virtual Sector Sector { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public string WorkOrder { get; set; }

    public string JobCode { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }

    public string Difficulty { get; set; }

    public string Goals { get; set; }

    

}