using E_Diary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Diary.Infrastructure.Persistence.Configurations
{
    public class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("TSubject");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("SubjectId");

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(256);

            builder.HasMany(t => t.SchoolerMarks)
                .WithOne(t => t.Subject)
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
