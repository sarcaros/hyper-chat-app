using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HyperChatApp.Core.Aggregates.RoomAggregate;

namespace HyperChatApp.Infrastructure.Data.ModelConfiguration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
  public void Configure(EntityTypeBuilder<Room> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(ModelConstants.NameLength)
      .IsRequired();

    builder.Property(p => p.PublicId)
      .HasMaxLength(ModelConstants.PublicIdLength)
      .IsRequired();

    builder.HasIndex(p => p.PublicId, "IDX_ROOM_PUBLICID").IsUnique();
  }
}

public class RoomAccessConfiguration : IEntityTypeConfiguration<RoomAccess>
{
  public void Configure(EntityTypeBuilder<RoomAccess> builder)
  {
    builder.Property(p => p.AccessLevel)
      .HasConversion(p => p.Value, p => AccessLevel.FromValue(p));
  }
}

