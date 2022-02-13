using E_Diary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Diary.Infrastructure.Persistence.EntityTypeConfiguration
{
    public class SchoolerMarkEntityTypeConfiguration : IEntityTypeConfiguration<SchoolerMark>
    {
        public void Configure(EntityTypeBuilder<SchoolerMark> builder)
        {
            builder.ToTable("TSchoolerMark");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("SchoolerMarkId");

            builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(t => t.Score)
                .HasColumnName("Score");

            builder
                .HasOne(sc => sc.Schooler)
                .WithMany(sm => sm.SchoolerMarks)
                .HasForeignKey(sm => sm.SchoolerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Subject)
                .WithMany(sm => sm.SchoolerMarks)
                .HasForeignKey(pc => pc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
