namespace HyperChatApp.Web.Endpoints.UserInfo;

public sealed class UpdateUserInfoResponse(string publicId, string name)
{
  public string PublicId { get; set; } = publicId;
  public string Name { get; set; } = name;
}
