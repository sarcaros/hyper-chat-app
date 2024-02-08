using FastEndpoints;
using FluentValidation;
using HyperChatApp.Infrastructure.Data;

namespace HyperChatApp.Web.Endpoints.Rooms;

public class CreateRoomValidator : Validator<CreateRoomRequest>
{
  public CreateRoomValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(ModelConstants.NameLength);
  }
}
