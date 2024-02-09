using System.Collections.Concurrent;
using HyperChatApp.Core.Interfaces;
using HyperChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HyperChatApp.Infrastructure.Auth;

public class UserInfoService(AppDbContext _db)
  : IUserInfoService
{
  private readonly ConcurrentDictionary<string, int> _publicToInternalIdMapping = new();
  private readonly ConcurrentDictionary<int, string> _internalToPublicIdMapping = new();

  public async Task<int?> GetUserIdByPublicIdAsync(string publicUserId)
  {
    if (_publicToInternalIdMapping.TryGetValue(publicUserId, out var id))
    {
      return id;
    }

    var result = await _db.Database.SqlQuery<int>($"SELECT Id FROM UserInfos WHERE PublicId = {publicUserId}").ToListAsync();

    if (result.Count == 1)
    {
      var resolvedId = result.First();
      _publicToInternalIdMapping.TryAdd(publicUserId, resolvedId);
      _internalToPublicIdMapping.TryAdd(resolvedId, publicUserId);
      return resolvedId;
    }

    return null;
  }

  public async Task<string?> GetUserPublicIdByIdAsync(int userId)
  {
    if (_internalToPublicIdMapping.TryGetValue(userId, out var publicId))
    {
      return publicId;
    }

    var result = await _db.Database.SqlQuery<string>($"SELECT PublicId FROM UserInfos WHERE Id = {userId}").ToListAsync();

    if (result.Count == 1)
    {
      var resolvedId = result.First();
      _internalToPublicIdMapping.TryAdd(userId, resolvedId);
      _publicToInternalIdMapping.TryAdd(resolvedId, userId);
      return resolvedId;
    }

    return null;
  }
}
