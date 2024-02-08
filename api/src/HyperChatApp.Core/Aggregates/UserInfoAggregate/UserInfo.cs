using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using NanoidDotNet;

namespace HyperChatApp.Core.Aggregates.UserInfoAggregate;

public class UserInfo(string name, string authUserId) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = name;
  public string AuthUserId { get; private set; } = authUserId;
  public string PublicId { get; private set; } = "u_" + Nanoid.Generate();

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName);
  }
}
