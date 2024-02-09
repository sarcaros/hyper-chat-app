namespace HyperChatApp.UseCases.Messages;

public record RoomMessageDTO(string PublicUserId, string Content, DateTimeOffset Time);
