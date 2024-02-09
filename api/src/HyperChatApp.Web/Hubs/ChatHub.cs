using HyperChatApp.Web.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HyperChatApp.Web.Hubs;

[Authorize]
public class ChatHub : Hub
{
  public async Task SendMessageToRoom(string publicRoomId, MessageContentRecord message)
  {
    await Clients.Groups(publicRoomId).SendAsync("SendMessage", message);
  }

  public async Task JoinRoom(string roomPublicId)
  {
    var userId = Context.User.GetInternalId();
    await Groups.AddToGroupAsync(Context.ConnectionId, roomPublicId);
    await SendMessageToRoom(roomPublicId, new($"{Context.ConnectionId} - {userId} has joined the group {roomPublicId}.", "#", TimeProvider.System.GetUtcNow()));
  }

  public async Task LeaveRoom(string roomPublicId)
  {
    var userId = Context.User.GetInternalId();
    await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomPublicId);
    await SendMessageToRoom(roomPublicId, new($"{Context.ConnectionId} - {userId} has left the group {roomPublicId}.", "#", TimeProvider.System.GetUtcNow()));
  }
}

