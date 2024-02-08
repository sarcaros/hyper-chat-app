namespace HyperChatApp.Web.Endpoints.Rooms;

public class CreateRoomRequest
{
  public const string Route = "/rooms";

  public string? Name { get; set; }
}
