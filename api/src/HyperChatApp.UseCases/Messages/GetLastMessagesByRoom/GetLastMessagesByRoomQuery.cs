using Ardalis.Result;
using Ardalis.SharedKernel;
using HyperChatApp.Core.Aggregates.MessageAggregate;

namespace HyperChatApp.UseCases.Messages.GetLastMessagesByRoom;

public record GetLastMessagesByRoomQuery(string PublicRoomId, int UserId, int Take) : IQuery<Result<IEnumerable<RoomMessageDTO>>>;
