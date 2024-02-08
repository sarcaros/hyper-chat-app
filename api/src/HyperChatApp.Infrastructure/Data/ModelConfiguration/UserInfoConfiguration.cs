using Microsoft.EntityFrameworkCore;
using HyperChatApp.Core.Aggregates.UserInfoAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperChatApp.Infrastructure.Data.ModelConfiguration;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
  public void Configure(EntityTypeBuilder<UserInfo> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(ModelConstants.NameLength)
      .IsRequired();

    builder.Property(p => p.AuthUserId)
      .HasMaxLength(ModelConstants.PublicIdLength)
      .IsRequired();

    builder.Property(p => p.PublicId)
      .HasMaxLength(ModelConstants.PublicIdLength)
      .IsRequired();

    builder.HasIndex(p => p.AuthUserId, "IDX_USERINFO_AUTHID").IsUnique();
  }
}
