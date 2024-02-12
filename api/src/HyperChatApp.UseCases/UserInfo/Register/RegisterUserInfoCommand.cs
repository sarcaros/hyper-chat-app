using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.UserInfo.Register;

public record RegisterUserInfoCommand(string AuthUserId, string UserName) : ICommand<Result<UserInfoDTO>>;
