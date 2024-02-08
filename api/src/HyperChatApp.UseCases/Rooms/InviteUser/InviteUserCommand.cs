using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HyperChatApp.UseCases.Rooms.InviteUser;

public record InviteUserCommand(string PublicRoomId, string PublicUserId, int CallerUserId) : ICommand<Result<PublicRoomAccessDTO>>;
