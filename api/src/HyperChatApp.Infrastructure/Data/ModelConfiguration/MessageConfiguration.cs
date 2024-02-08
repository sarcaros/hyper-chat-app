using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HyperChatApp.Core.Aggregates.MessageAggregate;

namespace HyperChatApp.Infrastructure.Data.ModelConfiguration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
  public void Configure(EntityTypeBuilder<Message> builder)
  {
    builder.Property(p => p.Content)
      .HasMaxLength(ModelConstants.MessageLength)
      .IsRequired();

    builder.Property(p => p.PublicId)
      .HasMaxLength(ModelConstants.PublicIdLength)
      .IsRequired();

    builder.HasIndex(p => p.RoomId, "IDX_MESSAGE_ROOMID").IsUnique();
  }
}

