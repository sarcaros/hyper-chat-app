using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;

namespace HyperChatApp.UseCases.Rooms.Get;

public class GetRoomsHandler(IRepository<Room> _roomRepository)
  : IQueryHandler<GetRoomsQuery, Result<IEnumerable<RoomDTO>>>
{
  public async Task<Result<IEnumerable<RoomDTO>>> Handle(GetRoomsQuery request, CancellationToken ct)
  {
    var spec = new RoomsWithAccessReadOnlySpec(request.UserId);
    var rooms = await _roomRepository.ListAsync(spec);

    return rooms.Select(x => new RoomDTO(x.PublicId, x.Name)).ToList();
  }
}
