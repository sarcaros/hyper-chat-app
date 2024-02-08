using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.Rooms.Get;

public record GetRoomsQuery(int UserId) : IQuery<Result<IEnumerable<RoomDTO>>>;
