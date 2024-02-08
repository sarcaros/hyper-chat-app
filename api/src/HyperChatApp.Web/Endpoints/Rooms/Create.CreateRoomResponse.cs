namespace HyperChatApp.Web.Endpoints.Rooms;

public sealed class CreateRoomResponse(string publicId, string name)
{
  public string PublicId { get; set; } = publicId;
  public string Name { get; set; } = name;
}
