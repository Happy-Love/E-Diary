using E_Diary.Domain.Entities;
using E_Diary.Infrastructure.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace E_Diary.Infrastructure.Persistence
{
    public class ApplicationDatabaseContext : DbContext
    {
        public DbSet<Schooler>? Persons { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<SchoolerMark>? Marks { get; set; }
        public DbSet<Group>? Groups { get; set; }

        private readonly DatabaseSettings settings;

        public ApplicationDatabaseContext(IOptions<DatabaseSettings> settings)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = settings?.ConnectionString ?? throw new ArgumentNullException(nameof(settings.ConnectionString));
            optionsBuilder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
