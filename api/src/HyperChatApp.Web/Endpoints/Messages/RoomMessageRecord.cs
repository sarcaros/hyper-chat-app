using HyperChatApp.Web.Endpoints.Messages;

public record RoomMessageRecord(string PublicUserId, string Content, DateTimeOffset Time);
