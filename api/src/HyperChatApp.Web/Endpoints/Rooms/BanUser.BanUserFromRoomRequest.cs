namespace HyperChatApp.Web.Endpoints.Rooms;

public class BanUserFromRoomRequest
{
  public const string Route = "/rooms/{PublicRoomId}/ban";

  public string? PublicRoomId { get; set; }
  public string? PublicUserId { get; set; }
}
