namespace HyperChatApp.Web.Endpoints.Messages;

public sealed class GetLastMessagesByRoomResponse
{
  public List<RoomMessageRecord> Messages { get; set; } = [];
}
