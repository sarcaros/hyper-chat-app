using Ardalis.Result;
using FastEndpoints;
using HyperChatApp.UseCases.Rooms.BanUser;
using HyperChatApp.UseCases.Rooms.Create;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class BanUser(IMediator _mediator)
  : Endpoint<BanUserFromRoomRequest, BanUserFromRoomResponse>
{
  public override void Configure()
  {
    Post(BanUserFromRoomRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(BanUserFromRoomRequest req, CancellationToken ct)
  {
    var callerUserId = User.GetInternalId();
    var result = await _mediator.Send(new BanUserCommand(req.PublicRoomId!, req.PublicUserId!, callerUserId));

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new(dto.PublicRoomId, dto.PublicUserId, dto.Level);
      return;
    }

    if (result.Status == ResultStatus.Forbidden)
    {
      await SendForbiddenAsync(ct);
      return;
    }

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }
  }
}

