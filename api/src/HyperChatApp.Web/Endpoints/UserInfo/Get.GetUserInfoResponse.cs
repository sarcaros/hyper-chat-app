namespace HyperChatApp.Web.Endpoints.UserInfo;

public sealed class GetUserInfoResponse(string publicId, string name)
{
  public string PublicId { get; set; } = publicId;
  public string Name { get; set; } = name;
}
