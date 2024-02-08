using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.UserInfo.Update;

public record UpdateUserInfoCommand(int UserId, string NewName) : ICommand<Result<UserInfoDTO>>;
