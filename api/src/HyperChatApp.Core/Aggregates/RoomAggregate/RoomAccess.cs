using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace HyperChatApp.Core.Aggregates.RoomAggregate;

public class RoomAccess(int userId, AccessLevel accessLevel) : EntityBase, IAggregateRoot
{
  public int UserId { get; private set; } = userId;
  public AccessLevel AccessLevel { get; private set; } = accessLevel;

  public void UpdateAccessLevel(AccessLevel newLevel)
  {
    AccessLevel = newLevel;
  }
}
