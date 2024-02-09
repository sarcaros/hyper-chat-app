namespace HyperChatApp.Core.Interfaces;

public interface IUserInfoService
{
  Task<int?> GetUserIdByPublicIdAsync(string publicUserId);

  Task<string?> GetUserPublicIdByIdAsync(int userId);
}
