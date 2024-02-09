using Ardalis.Specification;

namespace HyperChatApp.Core.Aggregates.MessageAggregate.Specifications;

public class LastMessagesByRoomReadOnlySpec : Specification<Message>
{
  public LastMessagesByRoomReadOnlySpec(int roomId, int take)
  {
    Query
      .AsNoTracking()
      .Where(x => x.RoomId == roomId)
      .OrderByDescending(x => x.Time)
      .Take(take);
  }
}
