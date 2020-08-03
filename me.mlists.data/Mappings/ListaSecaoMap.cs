using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.data.Mappings
{
    public sealed class ListaSecaoMap : IEntityTypeConfiguration<ListaSecao>
    {
        public void Configure(EntityTypeBuilder<ListaSecao> builder)
        {
            builder.ToTable("mltb_lista_secoes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("lista_secao_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao)
                .HasMaxLength(60)
                .IsRequired();
            builder.HasOne(t => t.Lista)
                .WithMany(f => f.ListaSecaos)
                .HasForeignKey(t => t.ListaId)
                .IsRequired();
        }
    }
}
