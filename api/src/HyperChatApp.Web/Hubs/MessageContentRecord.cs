namespace HyperChatApp.Web.Hubs;

public record MessageContentRecord(string Content, string PublicUserId, DateTimeOffset Time);
