using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;
using HyperChatApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace HyperChatApp.UseCases.Rooms.BanUser;

public class BanUserHandler(IRepository<Room> _roomRepository, IUserInfoService _userInfoService, ILogger<BanUserHandler> _logger)
  : ICommandHandler<BanUserCommand, Result<PublicRoomAccessDTO>>
{
  public async Task<Result<PublicRoomAccessDTO>> Handle(BanUserCommand command, CancellationToken ct)
  {
    _logger.LogInformation("Running {Handler} with {Command}", nameof(BanUserHandler), command);

    var bannedUserId = await _userInfoService.GetUserIdByPublicIdAsync(command.PublicUserId);
    if (bannedUserId is null)
    {
      _logger.LogWarning("To be banned user {UserId} not found", command.PublicUserId);
      return Result.NotFound("User id not found");
    }

    if (bannedUserId == command.CallerUserId)
    {
      _logger.LogWarning("Calling user is same as banned");
      return Result.Forbidden();
    }

    var roomSpec = new RoomByPublicIdSpec(command.PublicRoomId);
    var room = await _roomRepository.SingleOrDefaultAsync(roomSpec, ct);
    if (room is null)
    {
      _logger.LogWarning("Room {PublicRoomId} not found", command.PublicRoomId);
      return Result.NotFound("Room not found");
    }

    var callerAccess = room.GetUserAccess(command.CallerUserId);
    if (callerAccess?.IsAdmin != true)
    {
      _logger.LogWarning("User doesn't have rights. {UserId}", command.CallerUserId);
      return Result.Forbidden();
    }

    var bannedUserAccess = room.GetUserAccess(bannedUserId.Value);
    if (bannedUserAccess is null)
    {
      _logger.LogWarning("User is not a member of the room");
      return Result.NotFound("User is not a member of the room");
    }

    _logger.LogInformation("Ban user {UserId}", bannedUserId.Value);
    room.BanUser(bannedUserId.Value);

    await _roomRepository.UpdateAsync(room);
    _logger.LogInformation("User {UserId} banned in room {PublicRoomId}", bannedUserId.Value, room.PublicId);

    return new PublicRoomAccessDTO(room.PublicId, command.PublicUserId, bannedUserAccess.AccessLevel);
  }
}
