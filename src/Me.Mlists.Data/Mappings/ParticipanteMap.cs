using Me.Mlists.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Me.Mlists.Data.Mappings
{
    public sealed class ParticipanteMap : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.ToTable("mltb_participantes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("participante_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.UserId)
                .HasMaxLength(255)
                .IsRequired();
            builder.Ignore(x => x.User);
            builder.HasOne(t => t.Lista)
                .WithMany(f => f.Participantes)
                .HasForeignKey(t => t.ListaId)
                .IsRequired();
            builder.Property(t => t.ParticipantePerfil)
                .IsRequired();
        }
    }
}
