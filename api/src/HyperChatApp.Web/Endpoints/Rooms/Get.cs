using FastEndpoints;
using HyperChatApp.UseCases.Rooms.Get;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class GetRooms(IMediator _mediator)
  : Endpoint<EmptyRequest, GetRoomsResponse>
{
  public override void Configure()
  {
    Get(GetRoomsRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
  {
    var userId = User.GetInternalId();
    var rooms = await _mediator.Send(new GetRoomsQuery(userId));

    if (rooms.IsSuccess)
    {
      Response = new()
      {
        Rooms = rooms.Value.Select(r => new RoomRecord(r.PublicId, r.Name)).ToList(),
      };
      return;
    }
  }
}

