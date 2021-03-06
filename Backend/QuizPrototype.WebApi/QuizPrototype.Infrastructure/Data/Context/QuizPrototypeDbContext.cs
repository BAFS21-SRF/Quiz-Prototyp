using Microsoft.EntityFrameworkCore;
using QuizPrototype.Domain.Entities;
using QuizPrototype.Infrastructure.Data.Configurations;

namespace QuizPrototype.Infrastructure.Data.Context
{
    public class QuizPrototypeDbContext : DbContext
    {
        public DbSet<Frage> Frage { get; set; }
        public DbSet<Auswahl> Auswahl { get; set; }

        public DbSet<Game> Game { get; set; }

        public QuizPrototypeDbContext(DbContextOptions<QuizPrototypeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new FrageConfiguration());

            builder
                .ApplyConfiguration(new AuswahlConfiguration());

            builder
                .ApplyConfiguration(new GameConfiguration());
        }

    }
}
