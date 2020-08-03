using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace me.mlists.data.Mappings
{
    public sealed class ListaMap : IEntityTypeConfiguration<Lista>
    {
        public void Configure(EntityTypeBuilder<Lista> builder)
        {
            builder.ToTable("mltb_listas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("lista_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.CriadoPorId)
                .HasMaxLength(255)
                .IsRequired();
            builder.Ignore(x => x.CriadoPor);
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasOne(t => t.Monster)
                .WithMany(f => f.Listas)
                .HasForeignKey(t => t.MonsterId)
                .IsRequired();
            builder.Property(t => t.ListaStatus)
                .IsRequired();
        }
    }
}
