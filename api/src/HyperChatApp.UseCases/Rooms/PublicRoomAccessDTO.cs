using HyperChatApp.Core.Aggregates.RoomAggregate;

namespace HyperChatApp.UseCases.Rooms;

public record PublicRoomAccessDTO(string PublicRoomId, string PublicUserId, AccessLevel Level);
