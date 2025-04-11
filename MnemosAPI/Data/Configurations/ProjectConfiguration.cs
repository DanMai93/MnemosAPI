﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MnemosAPI.Data;
using MnemosAPI.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MnemosAPI.Data.Configurations
{
    public partial class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.Property(e => e.Difficulty)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.EndCustomer).HasMaxLength(50);
            entity.Property(e => e.Goals).IsUnicode(false);
            entity.Property(e => e.JobCode).HasMaxLength(30);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Customers");

            entity.HasOne(d => d.Role).WithMany(p => p.Projects)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Projects_Roles");

            entity.HasOne(d => d.Sector).WithMany(p => p.Projects)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK_Projects_Sectors");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Users");


            entity.HasMany(p => p.Skills)
                .WithMany(s => s.Projects)
                .UsingEntity<Dictionary<string, object>>(
                "ProjectSkill",
                j => j.HasOne<Skill>().WithMany(),
                j => j.HasOne<Project>().WithMany());






            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Project> entity);
    }
}
