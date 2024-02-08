using Ardalis.Result;
using Ardalis.SharedKernel;

using Entities = HyperChatApp.Core.Aggregates;

namespace HyperChatApp.UseCases.UserInfo.Update;

public class UpdateUserInfoHandler(IRepository<Entities.UserInfoAggregate.UserInfo> _userRepository)
  : ICommandHandler<UpdateUserInfoCommand, Result<UserInfoDTO>>
{
  public async Task<Result<UserInfoDTO>> Handle(UpdateUserInfoCommand request, CancellationToken ct)
  {
    var userInfo = await _userRepository.GetByIdAsync(request.UserId, ct);
    if (userInfo == null)
    {
      return Result.NotFound();
    }

    userInfo.UpdateName(request.NewName);

    await _userRepository.UpdateAsync(userInfo, ct);

    return new UserInfoDTO(userInfo.Id, userInfo.Name, userInfo.PublicId);
  }
}
