using Ardalis.SmartEnum;

namespace HyperChatApp.Core.Aggregates.RoomAggregate;

public class AccessLevel : SmartEnum<AccessLevel>
{
  public static readonly AccessLevel Owner = new(nameof(Owner), 1);
  public static readonly AccessLevel Mod = new(nameof(Owner), 2);
  public static readonly AccessLevel ReadOnly = new(nameof(Owner), 10);
  public static readonly AccessLevel Write = new(nameof(Owner), 20);
  public static readonly AccessLevel Invited = new(nameof(Owner), 100);
  public static readonly AccessLevel Banned = new(nameof(Owner), 666);

  protected AccessLevel(string name, int value) : base(name, value)
  {
  }
}
