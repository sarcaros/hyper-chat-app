using HyperChatApp.Core.Aggregates.RoomAggregate;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class BanUserFromRoomResponse(string publicRoomId, string publicUserId, AccessLevel level)
{
  public string PublicRoomId { get; set; } = publicRoomId;
  public string PublicUserId { get; set; } = publicUserId;
  public AccessLevel AccessLevel { get; set; } = level;
}
