namespace HyperChatApp.Infrastructure.Auth;

public interface IUserAuthService
{
  Task<int?> GetUserIdAsync(string authUserId);
}
