using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace HyperChatApp.Core.Aggregates.RoomAggregate;

public class RoomAccess(int userId, AccessLevel accessLevel) : EntityBase, IAggregateRoot
{
  public int UserId { get; private set; } = userId;
  public AccessLevel AccessLevel { get; private set; } = accessLevel;

  public bool IsAdmin => AccessLevel == AccessLevel.Mod || AccessLevel == AccessLevel.Owner;
  public bool CanWrite => IsAdmin || AccessLevel == AccessLevel.Write;
  public bool CanRead => AccessLevel != AccessLevel.Banned;

  public void UpdateAccessLevel(AccessLevel newLevel)
  {
    AccessLevel = newLevel;
  }
}
