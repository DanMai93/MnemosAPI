﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MnemosAPI.Data;
using MnemosAPI.Models;
using System;
using System.Collections.Generic;

namespace MnemosAPI.Data.Configurations
{
    public partial class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> entity)
        {
            entity.ToTable("Skill");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Area).WithMany(p => p.Skills)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill_Area");

            entity.HasOne(d => d.Scale).WithMany(p => p.Skills)
                .HasForeignKey(d => d.ScaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill_Scale");

            entity.HasMany(d => d.Projects)
                .WithMany(p => p.Skills);

            entity.HasMany(d => d.Categories)
                .WithMany(p => p.Skills);


            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Skill> entity);
    }
}
