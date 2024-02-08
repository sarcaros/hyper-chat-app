using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.RoomAggregate;

namespace HyperChatApp.UseCases.Rooms.Create;

public class CreateRoomHandler(IRepository<Room> _roomRepository)
  : ICommandHandler<CreateRoomCommand, Result<RoomDTO>>
{
  public async Task<Result<RoomDTO>> Handle(CreateRoomCommand request, CancellationToken ct)
  {
    Room room = new(request.Name);
    room.SetOwner(request.OwnerId);

    await _roomRepository.AddAsync(room, ct);

    return new RoomDTO(room.PublicId, room.Name);
  }
}
