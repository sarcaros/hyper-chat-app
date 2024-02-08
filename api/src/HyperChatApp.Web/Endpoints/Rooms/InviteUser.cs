using Ardalis.Result;
using FastEndpoints;
using HyperChatApp.UseCases.Rooms.InviteUser;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class InviteUser(IMediator _mediator)
  : Endpoint<InviteUserToRoomRequest, InviteUserToRoomResponse>
{
  public override void Configure()
  {
    Post(InviteUserToRoomRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(InviteUserToRoomRequest req, CancellationToken ct)
  {
    var callerUserId = User.GetInternalId();
    var result = await _mediator.Send(new InviteUserCommand(req.PublicRoomId!, req.PublicUserId!, callerUserId));

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

