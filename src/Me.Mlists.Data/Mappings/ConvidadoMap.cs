﻿using Me.Mlists.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Me.Mlists.Data.Mappings
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
            builder.Property(c => c.DataEnvio)
                .IsRequired();
            builder.Property(c => c.ConvidadoPorId)
                .IsRequired();
            builder.Ignore(c => c.UserId);
        }
    }
}
