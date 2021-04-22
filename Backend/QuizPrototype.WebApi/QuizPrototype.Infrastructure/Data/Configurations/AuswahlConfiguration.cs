using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.Infrastructure.Data.Configurations
{
    class AuswahlConfiguration : IEntityTypeConfiguration<Auswahl>
    {
        public void Configure(EntityTypeBuilder<Auswahl> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AuswahlText)
                .IsRequired()
                .HasColumnName("AuswahlText");

            builder.Property(a => a.Order)
                .IsRequired()
                .HasColumnName("Order");

            builder.Property(a => a.FrageId)
                .IsRequired()
                .HasColumnName("FrageId");

            builder.Property(a => a.AssetId)
                .IsRequired()
                .HasColumnName("AssetId");
        }
    }
}
