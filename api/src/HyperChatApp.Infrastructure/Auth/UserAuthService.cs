using System.Collections.Concurrent;
using HyperChatApp.Core.Interfaces;
using HyperChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HyperChatApp.Infrastructure.Auth;

public class UserInfoService(AppDbContext _db)
  : IUserInfoService
{
  private readonly ConcurrentDictionary<string, int> _mapping = new ConcurrentDictionary<string, int>();

  public async Task<int?> GetUserIdByPublicIdAsync(string publicUserId)
  {
    if (_mapping.TryGetValue(publicUserId, out var id))
    {
      return id;
    }

    var result = await _db.Database.SqlQuery<int>($"SELECT Id FROM UserInfos WHERE PublicId = {publicUserId}").ToListAsync();

    if (result.Count == 1)
    {
      var resolvedId = result.First();
      _mapping.TryAdd(publicUserId, resolvedId);
      return resolvedId;
    }

    return null;
  }
}
