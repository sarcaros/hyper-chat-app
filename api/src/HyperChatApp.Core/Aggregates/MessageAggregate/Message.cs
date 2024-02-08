using Ardalis.SharedKernel;
using NanoidDotNet;

namespace HyperChatApp.Core.Aggregates.MessageAggregate;

public class Message(int userId, int roomId, string content) : EntityBase, IAggregateRoot
{
  public int UserId { get; private set; } = userId;
  public int RoomId { get; private set; } = roomId;
  public string Content { get; private set; } = content;
  public string PublicId { get; private set; } = "m_" + Nanoid.Generate();
}
