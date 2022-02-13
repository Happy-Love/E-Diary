using E_Diary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Diary.Infrastructure.Persistence.Configurations
{
    public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("TGroup");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("GroupId");

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(256);

            builder.HasMany(t=>t.Schoolers)
                .WithOne(t=>t.Group)
                .HasForeignKey(t=>t.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
