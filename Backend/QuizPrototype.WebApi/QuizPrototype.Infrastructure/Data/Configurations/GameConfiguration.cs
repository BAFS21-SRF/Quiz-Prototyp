using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.Infrastructure.Data.Configurations
{
    class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Guid)
                .IsRequired()
                .HasColumnName("Guid");

            builder.Property(a => a.AktuelleFrageId)
                .IsRequired()
                .HasColumnName("AktuelleFrageId");
        }
    }
}
