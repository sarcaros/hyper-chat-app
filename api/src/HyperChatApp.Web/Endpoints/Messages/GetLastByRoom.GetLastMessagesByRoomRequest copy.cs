using HyperChatApp.Web.Endpoints.Messages;

public class GetLastMessagesByRoomRequest
{
  public const string Route = "/messages";

  public string? RoomId { get; set; }
  public int? Take { get; set; }
}
