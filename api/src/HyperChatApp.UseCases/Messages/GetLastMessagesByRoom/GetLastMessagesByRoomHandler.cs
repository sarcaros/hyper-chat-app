using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.MessageAggregate;
using HyperChatApp.Core.Aggregates.MessageAggregate.Specifications;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;
using HyperChatApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace HyperChatApp.UseCases.Messages.GetLastMessagesByRoom;

public class GetLastMessagesByRoomHandler
(
  IRepository<Message> _messageRepository,
  IRepository<Room> _roomRepository,
  IUserInfoService _userInfoService,
  ILogger<GetLastMessagesByRoomHandler> _logger
) : IQueryHandler<GetLastMessagesByRoomQuery, Result<IEnumerable<RoomMessageDTO>>>
{
  public async Task<Result<IEnumerable<RoomMessageDTO>>> Handle(GetLastMessagesByRoomQuery request, CancellationToken ct)
  {
    var roomSpec = new RoomByPublicIdSpec(request.PublicRoomId);
    var room = await _roomRepository.SingleOrDefaultAsync(roomSpec, ct);
    if (room is null)
    {
      _logger.LogWarning("Room {PublicRoomId} not found", request.PublicRoomId);
      return Result.NotFound("Room not found");
    }

    var callerAccess = room.GetUserAccess(request.UserId);
    if (callerAccess?.CanRead != true)
    {
      _logger.LogWarning("User doesn't have rights to read messages. {UserId}", request.UserId);
      return Result.Forbidden();
    }

    var messagesSpec = new LastMessagesByRoomReadOnlySpec(room.Id, request.Take);
    var messages = await _messageRepository.ListAsync(messagesSpec, ct);

    List<RoomMessageDTO> result = new(messages.Count);

    foreach (var message in messages)
    {
      var publicUserId = await _userInfoService.GetUserPublicIdByIdAsync(message.UserId) ?? "u_???";
      result.Add(new(publicUserId, message.Content, message.Time));
    }

    return result;
  }
}
