using Ardalis.Result;
using FastEndpoints;
using HyperChatApp.UseCases.UserInfo.Get;
using HyperChatApp.Web.Auth;
using MediatR;

namespace HyperChatApp.Web.Endpoints.UserInfo;

public class GetUserInfo(IMediator _mediator)
  : Endpoint<EmptyRequest, GetUserInfoResponse>
{
  public override void Configure()
  {
    Get(GetUserInfoRequest.Route);
    Claims(ClaimsConstants.InternalIdClaim);
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
  {
    var userId = User.GetInternalId();
    var result = await _mediator.Send(new GetUserInfoQuery(userId));

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new GetUserInfoResponse(dto.PublicId, dto.Name);
    }
  }
}
