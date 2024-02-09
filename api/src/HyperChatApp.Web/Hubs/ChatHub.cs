using HyperChatApp.UseCases.Messages.Save;
using HyperChatApp.Web.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HyperChatApp.Web.Hubs;

[Authorize]
public class ChatHub : Hub
{
  public async Task SendMessageToRoom(string publicRoomId, string content, [FromServices] IMediator _mediator)
  {
    var userId = Context.User.GetInternalId();
    var result = await _mediator.Send(new SaveMessageCommand(publicRoomId, content, userId));
    if (result.IsSuccess)
    {
      var dto = result.Value;
      await SendMessageToRoomWithoutSave(publicRoomId, new MessageContentRecord(dto.Content, dto.PublicUserId, dto.Time));
      return;
    }

    await Clients.Caller.SendAsync("SendMessage", new MessageContentRecord($"Error processing message - {result.Status}", "!", TimeProvider.System.GetUtcNow()));
  }

  public async Task JoinRoom(string roomPublicId)
  {
    var userId = Context.User.GetInternalId();
    await Groups.AddToGroupAsync(Context.ConnectionId, roomPublicId);
    await SendMessageToRoomWithoutSave(roomPublicId, new($"{Context.ConnectionId} - {userId} has joined the group {roomPublicId}.", "#", TimeProvider.System.GetUtcNow()));
  }

  public async Task LeaveRoom(string roomPublicId)
  {
    var userId = Context.User.GetInternalId();
    await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomPublicId);
    await SendMessageToRoomWithoutSave(roomPublicId, new($"{Context.ConnectionId} - {userId} has left the group {roomPublicId}.", "#", TimeProvider.System.GetUtcNow()));
  }

  private async Task SendMessageToRoomWithoutSave(string publicRoomId, MessageContentRecord message)
  {
    await Clients.Groups(publicRoomId).SendAsync("SendMessage", message);
  }
}

