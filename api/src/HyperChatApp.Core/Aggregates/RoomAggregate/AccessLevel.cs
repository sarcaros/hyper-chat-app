using Ardalis.SmartEnum;

namespace HyperChatApp.Core.Aggregates.RoomAggregate;

public class AccessLevel : SmartEnum<AccessLevel>
{
  public static readonly AccessLevel Owner = new(nameof(Owner), 1);
  public static readonly AccessLevel Mod = new(nameof(Mod), 2);
  public static readonly AccessLevel ReadOnly = new(nameof(ReadOnly), 10);
  public static readonly AccessLevel Write = new(nameof(Write), 20);
  public static readonly AccessLevel Invited = new(nameof(Invited), 100);
  public static readonly AccessLevel Banned = new(nameof(Banned), 666);

  protected AccessLevel(string name, int value) : base(name, value)
  {
  }
}
