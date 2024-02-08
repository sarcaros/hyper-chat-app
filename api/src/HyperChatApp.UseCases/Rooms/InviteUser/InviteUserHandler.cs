using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate.Specifications;
using HyperChatApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace HyperChatApp.UseCases.Rooms.InviteUser;

public class InviteUserHandler(IRepository<Room> _roomRepository, IUserInfoService _userInfoService, ILogger<InviteUserHandler> _logger)
  : ICommandHandler<InviteUserCommand, Result<PublicRoomAccessDTO>>
{
  public async Task<Result<PublicRoomAccessDTO>> Handle(InviteUserCommand command, CancellationToken ct)
  {
    _logger.LogInformation("Running {Handler} with {Command}", nameof(InviteUserHandler), command);

    var invitedUserId = await _userInfoService.GetUserIdByPublicIdAsync(command.PublicUserId);
    if (invitedUserId is null)
    {
      _logger.LogWarning("To be invited user {UserId} not found", command.PublicUserId);
      return Result.NotFound("User id not found");
    }

    if (invitedUserId == command.CallerUserId)
    {
      _logger.LogWarning("Calling user is same as invited");
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
      _logger.LogWarning("User doesn't have rights. {Rights}", callerAccess);
      return Result.Forbidden();
    }

    var invitedUserAccess = room.GetUserAccess(invitedUserId.Value);
    if (invitedUserAccess is not null)
    {
      _logger.LogWarning("User is a member of the room. {Rights}", invitedUserAccess);
      return Result.NotFound("User is already member of the room");
    }

    _logger.LogInformation("Invite user {UserId}", invitedUserId.Value);

    room.InviteUser(invitedUserId.Value);
    invitedUserAccess = room.GetUserAccess(invitedUserId.Value);

    await _roomRepository.UpdateAsync(room, ct);
    _logger.LogInformation("User {UserId} invited to room {PublicRoomId}", invitedUserId.Value, room.PublicId);


    return new PublicRoomAccessDTO(room.PublicId, command.PublicUserId, invitedUserAccess!.AccessLevel);
  }
}
