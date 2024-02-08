using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.Rooms.BanUser;

public record BanUserCommand(string PublicRoomId, string PublicUserId, int CallerUserId) : ICommand<Result<PublicRoomAccessDTO>>;
