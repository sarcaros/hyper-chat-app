using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.Rooms.Create;

public record CreateRoomCommand(int OwnerId, string Name) : ICommand<Result<RoomDTO>>;
