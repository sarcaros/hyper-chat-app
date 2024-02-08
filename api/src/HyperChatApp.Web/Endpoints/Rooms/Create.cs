using Ardalis.Result;
using FastEndpoints;
using HyperChatApp.UseCases.Rooms.Create;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class CreateRoom(IMediator _mediator)
  : Endpoint<CreateRoomRequest, CreateRoomResponse>
{
  public override void Configure()
  {
    Post(CreateRoomRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(CreateRoomRequest req, CancellationToken ct)
  {
    var userId = User.GetInternalId();
    var result = await _mediator.Send(new CreateRoomCommand(userId, req.Name!));

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new(dto.PublicId, dto.Name);
      return;
    }
  }
}

