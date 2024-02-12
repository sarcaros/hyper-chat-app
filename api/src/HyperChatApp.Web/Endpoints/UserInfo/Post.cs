using System.Text;
using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Security;
using HyperChatApp.UseCases.UserInfo.Register;

using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace HyperChatApp.Web.Endpoints.UserInfo;

public class RegisterUserInfo(IMediator _mediator, IHttpClientFactory _httpClientFactory)
  : Endpoint<EmptyRequest>
{
  public override void Configure()
  {
    Post(GetUserInfoRequest.Route);
    Claims("sub");
  }

  public override async Task HandleAsync(EmptyRequest _, CancellationToken ct)
  {
    var userId = User.ClaimValue("sub");
    var userName = User.ClaimValue("name");

    var result = await _mediator.Send(new RegisterUserInfoCommand(userId!, userName!));

    if (result.IsSuccess)
    {
      var dto = result.Value;
      using var client = _httpClientFactory.CreateClient("clerk");
      var content = JsonSerializer.Serialize(new { public_metadata = new { internal_uid = dto.Id } });
      var metadata = new StringContent(
              content,
              Encoding.UTF8,
              Application.Json);

      var response = await client.PatchAsync($"users/{userId}/metadata", metadata);
      var text = await response.Content.ReadAsStringAsync();
      response.EnsureSuccessStatusCode().WriteRequestToConsole();

      await SendNoContentAsync(ct);
    }
  }
}


static class HttpResponseMessageExtensions
{
  internal static void WriteRequestToConsole(this HttpResponseMessage response)
  {
    if (response is null)
    {
      return;
    }

    var request = response.RequestMessage;
    Console.Write($"{request?.Method} ");
    Console.Write($"{request?.RequestUri} ");
    Console.WriteLine($"HTTP/{request?.Version}");
  }
}
