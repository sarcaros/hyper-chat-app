using Ardalis.Specification;

namespace HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;

public class RoomByPublicIdSpec : SingleResultSpecification<Room>
{
  public RoomByPublicIdSpec(string publicId)
  {
    Query
      .Where(x => x.PublicId == publicId)
      .Include(x => x.RoomAccesses);
  }
}
