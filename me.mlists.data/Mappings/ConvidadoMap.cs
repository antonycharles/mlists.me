using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.data.Mappings
{
    public sealed class ConvidadoMap : IEntityTypeConfiguration<Convidado>
    {
        public void Configure(EntityTypeBuilder<Convidado> builder)
        {
            builder.ToTable("mltb_convidados");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("convidado_id")
                .ValueGeneratedOnAdd();
            builder.HasOne(t => t.Lista)
                .WithMany(f => f.Convidados)
                .HasForeignKey(t => t.ListaId)
                .IsRequired();
            builder.Property(c => c.EmailConvite)
                .IsRequired()
                .HasColumnName("email_convidado")
                .HasMaxLength(100);
        }
    }
}
