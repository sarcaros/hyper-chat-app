namespace HyperChatApp.Core.Interfaces;

public interface IUserInfoService
{
  Task<int?> GetUserIdByPublicIdAsync(string publicUserId);
}
