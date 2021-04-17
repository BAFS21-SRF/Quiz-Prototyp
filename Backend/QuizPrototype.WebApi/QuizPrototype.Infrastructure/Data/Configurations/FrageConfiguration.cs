using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPrototype.Domain.Entities;

namespace QuizPrototype.Infrastructure.Data.Configurations
{
    class FrageConfiguration : IEntityTypeConfiguration<Frage>
    {
        public void Configure(EntityTypeBuilder<Frage> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FrageText)
                .IsRequired()
                .HasColumnName("FrageText");

            builder.HasMany(f => f.Auswahlmoeglichkeiten)
                .WithOne()
                .HasForeignKey("FrageId");
        }
    }
}
