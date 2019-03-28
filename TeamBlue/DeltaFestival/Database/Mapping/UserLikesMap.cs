using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Mapping
{
    class UserLikesMap : IEntityTypeConfiguration<UserLikes>
    {
        public void Configure(EntityTypeBuilder<UserLikes> builder)
        {
            builder.HasKey(p => new { p.UserId, p.UserLikedId });
            builder.Property(p => p.IsVoted).HasDefaultValue(true);
        }
    }
}
