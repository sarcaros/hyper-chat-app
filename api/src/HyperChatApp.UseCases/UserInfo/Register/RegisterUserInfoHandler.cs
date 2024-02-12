
using Ardalis.Result;
using Ardalis.SharedKernel;

using Entities = HyperChatApp.Core.Aggregates;

namespace HyperChatApp.UseCases.UserInfo.Register;

public class UpdateUserInfoHandler(IRepository<Entities.UserInfoAggregate.UserInfo> _userRepository)
  : ICommandHandler<RegisterUserInfoCommand, Result<UserInfoDTO>>
{

  public async Task<Result<UserInfoDTO>> Handle(RegisterUserInfoCommand request, CancellationToken ct)
  {
    Entities.UserInfoAggregate.UserInfo info = new(request.UserName, request.AuthUserId);

    await _userRepository.AddAsync(info, ct);

    return new UserInfoDTO(info.Id, info.Name, info.PublicId);
  }
}
