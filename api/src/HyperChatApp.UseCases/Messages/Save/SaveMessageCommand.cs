using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.Messages.Save;

public record SaveMessageCommand(string PublicRoomId, string Content, int UserId) : ICommand<Result<RoomMessageDTO>>;
