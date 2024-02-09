using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.MessageAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;
using HyperChatApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace HyperChatApp.UseCases.Messages.Save;

public class SaveMessageHandler
(
  IRepository<Message> _messageRepository,
  IRepository<Room> _roomRepository,
  IUserInfoService _userInfoService,
  ILogger<SaveMessageHandler> _logger
) : ICommandHandler<SaveMessageCommand, Result<RoomMessageDTO>>
{
  public async Task<Result<RoomMessageDTO>> Handle(SaveMessageCommand request, CancellationToken ct)
  {
    var roomSpec = new RoomByPublicIdSpec(request.PublicRoomId);
    var room = await _roomRepository.SingleOrDefaultAsync(roomSpec, ct);
    if (room is null)
    {
      _logger.LogWarning("Room {PublicRoomId} not found", request.PublicRoomId);
      return Result.NotFound("Room not found");
    }

    var callerAccess = room.GetUserAccess(request.UserId);
    if (callerAccess?.CanWrite != true)
    {
      _logger.LogWarning("User doesn't have rights to write messages. {UserId}", request.UserId);
      return Result.Forbidden();
    }

    var message = new Message(request.UserId, room.Id, request.Content);
    await _messageRepository.AddAsync(message);

    var publicUserId = await _userInfoService.GetUserPublicIdByIdAsync(message.UserId) ?? "u_???";

    return new RoomMessageDTO(publicUserId, message.Content, message.Time);
  }
}
