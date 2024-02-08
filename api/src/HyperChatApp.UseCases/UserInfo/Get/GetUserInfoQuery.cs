using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.UserInfo.Get;

public record GetUserInfoQuery(int UserId) : IQuery<Result<UserInfoDTO>>;
