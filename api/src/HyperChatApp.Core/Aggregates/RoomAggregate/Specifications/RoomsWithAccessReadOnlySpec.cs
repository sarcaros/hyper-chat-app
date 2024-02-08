using Ardalis.Specification;

namespace HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;

public class RoomsWithAccessReadOnlySpec : Specification<Room>
{
  public RoomsWithAccessReadOnlySpec(int userId)
  {
    Query
      .Where(x => x.RoomAccesses.Any(a => a.UserId == userId && a.AccessLevel != AccessLevel.Banned));
  }
}
