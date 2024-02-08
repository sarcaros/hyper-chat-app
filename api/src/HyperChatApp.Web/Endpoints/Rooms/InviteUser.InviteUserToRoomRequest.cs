using Microsoft.AspNetCore.Mvc;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class InviteUserToRoomRequest
{
  public const string Route = "/rooms/{publicRoomId}/invite";

  public string? PublicRoomId { get; set; }
  public string? PublicUserId { get; set; }
}
