using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using NanoidDotNet;

namespace HyperChatApp.Core.Aggregates.RoomAggregate;

public class Room(string name) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = name;
  public string PublicId { get; private set; } = "r_" + Nanoid.Generate();
  public IEnumerable<RoomAccess> RoomAccesses => _roomAccesses.AsReadOnly();

  private readonly List<RoomAccess> _roomAccesses = new();

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName);
  }

  public void SetOwner(int userId)
  {
    if (OwnerAccess is not null)
    {
      throw new OwnerAlreadySetException();
    }
    _roomAccesses.Add(new(userId, AccessLevel.Owner));
  }

  public void BanUser(int userId)
  {
    var access = GetUserAccess(userId) ?? throw new AccessNotExistsException();
    access.UpdateAccessLevel(AccessLevel.Banned);
  }

  public void InviteUser(int userId)
  {
    var access = GetUserAccess(userId);
    if (access is not null)
    {
      throw new UserAlreadyInvitedException();
    }

    _roomAccesses.Add(new(userId, AccessLevel.Write));
  }

  public RoomAccess? GetUserAccess(int userId)
    => RoomAccesses.SingleOrDefault(x => x.UserId == userId);

  private RoomAccess? OwnerAccess
  => RoomAccesses.SingleOrDefault(x => x.AccessLevel == AccessLevel.Owner);

  public class OwnerAlreadySetException : InvalidOperationException
  { }

  public class AccessNotExistsException : InvalidOperationException
  { }

  public class UserAlreadyInvitedException : InvalidOperationException
  { }
}
