using FastEndpoints;

namespace HyperChatApp.Web.Endpoints.UserInfo;

public sealed class UpdateUserInfoRequest
{
  public const string Route = "/userinfo";

  public string? Name { get; set; }
}
