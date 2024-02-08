using Ardalis.Specification;

namespace HyperChatApp.Core.Aggregates.UserInfoAggregate.Specifications;

public class UserInfoByIdReadOnlySpec : Specification<UserInfo>
{
  public UserInfoByIdReadOnlySpec(int id)
  {
    Query
      .Where(u => u.Id == id)
      .AsNoTracking();
  }
}
