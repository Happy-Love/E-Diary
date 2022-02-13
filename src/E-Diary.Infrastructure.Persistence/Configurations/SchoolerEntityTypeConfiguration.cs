using E_Diary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Diary.Infrastructure.Persistence.Configurations
{
    public class SchoolerEntityTypeConfiguration : IEntityTypeConfiguration<Schooler>
    {
        public void Configure(EntityTypeBuilder<Schooler> builder)
        {
            builder.ToTable("TSchooler");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("SchoolerId");

            builder.Property(t => t.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(256);

            builder.Property(t => t.MiddleName)
                .HasColumnName("MiddleName")
                .HasMaxLength(256);

            builder.Property(t => t.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(256);

            builder.HasMany(t => t.SchoolerMarks)
                .WithOne(t => t.Schooler)
                .HasForeignKey(t => t.SchoolerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
