using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.UserInfoAggregate.Specifications;
using Entities = HyperChatApp.Core.Aggregates;

namespace HyperChatApp.UseCases.UserInfo.Get;

public class GetUserInfoHandler(IRepository<Entities.UserInfoAggregate.UserInfo> _userRepository)
  : IQueryHandler<GetUserInfoQuery, Result<UserInfoDTO>>
{
  public async Task<Result<UserInfoDTO>> Handle(GetUserInfoQuery request, CancellationToken ct)
  {
    var spec = new UserInfoByIdReadOnlySpec(request.UserId);
    var userInfo = await _userRepository.FirstOrDefaultAsync(spec, ct);
    if (userInfo == null)
    {
      return Result.NotFound();
    }

    return new UserInfoDTO(userInfo.Id, userInfo.Name, userInfo.PublicId);
  }
}
