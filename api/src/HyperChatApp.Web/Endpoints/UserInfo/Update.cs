using Ardalis.Result;
using FastEndpoints;
using HyperChatApp.UseCases.UserInfo.Update;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.UserInfo;

public class UpdateUserInfo(IMediator _mediator)
  : Endpoint<UpdateUserInfoRequest, UpdateUserInfoResponse>
{
  public override void Configure()
  {
    Patch(GetUserInfoRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(UpdateUserInfoRequest req, CancellationToken ct)
  {
    var userId = User.GetInternalId();
    var result = await _mediator.Send(new UpdateUserInfoCommand(userId, req.Name!));

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new UpdateUserInfoResponse(dto.PublicId, dto.Name);
    }
  }
}
