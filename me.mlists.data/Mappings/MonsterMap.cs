using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.data.Mappings
{
    public sealed class MonsterMap : IEntityTypeConfiguration<Monster>
    {
        public void Configure(EntityTypeBuilder<Monster> builder)
        {
            builder.ToTable("mltb_monsters");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("monster_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Url)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
