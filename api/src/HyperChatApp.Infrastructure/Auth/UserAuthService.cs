using System.Collections.Concurrent;
using HyperChatApp.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HyperChatApp.Infrastructure.Auth;

public class UserAuthService(AppDbContext _db)
  : IUserAuthService
{
  private readonly ConcurrentDictionary<string, int> _mapping = new ConcurrentDictionary<string, int>();

  public async Task<int?> GetUserIdAsync(string authUserId)
  {
    if (_mapping.TryGetValue(authUserId, out var id))
    {
      return id;
    }

    var result = await _db.UserInfos.Where(x => x.AuthUserId == authUserId)
      .Select(x => x.Id)
      .FirstOrDefaultAsync();

    if (result > 0)
    {
      _mapping.TryAdd(authUserId, result);
      return result;
    }

    return null;
  }
}
