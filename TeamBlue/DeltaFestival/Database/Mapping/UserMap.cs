using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TicketCode).HasMaxLength(50);
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            builder.Property(p => p.LikeScore).HasDefaultValue(0);
        }
    }
}
