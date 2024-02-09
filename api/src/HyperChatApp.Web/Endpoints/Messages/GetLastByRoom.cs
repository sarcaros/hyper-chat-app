using FastEndpoints;
using HyperChatApp.UseCases.Messages.GetLastMessagesByRoom;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.Messages;

public class GetLastByRoom(IMediator _mediator)
  : Endpoint<GetLastMessagesByRoomRequest, GetLastMessagesByRoomResponse>
{
  public override void Configure()
  {
    Get(GetLastMessagesByRoomRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(GetLastMessagesByRoomRequest req, CancellationToken ct)
  {
    var userId = User.GetInternalId();
    var rooms = await _mediator.Send(new GetLastMessagesByRoomQuery(req.RoomId!, userId, req.Take ?? 10));

    if (rooms.IsSuccess)
    {
      Response = new()
      {
        Messages = rooms.Value.Select(r => new RoomMessageRecord(r.PublicUserId, r.Content, r.Time)).ToList(),
      };
      return;
    }
  }
}

