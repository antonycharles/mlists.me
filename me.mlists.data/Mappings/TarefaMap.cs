using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.data.Mappings
{
    public sealed class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("mltb_tarefas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("tarefa_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Nome)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(c => c.IsChecked)
                .IsRequired();
            builder.Property(c => c.IsLixeira)
                .IsRequired();
            builder.HasOne(t => t.Participante)
                .WithMany(f => f.Tarefas)
                .HasForeignKey(t => t.ParticipanteId)
                .IsRequired();
            builder.HasOne(t => t.ListaSecao)
                .WithMany(f => f.Tarefas)
                .HasForeignKey(t => t.ListaSecaoId);
        }
    }
}
