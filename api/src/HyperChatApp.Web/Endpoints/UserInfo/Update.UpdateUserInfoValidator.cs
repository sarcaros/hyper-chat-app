using FastEndpoints;
using FluentValidation;
using HyperChatApp.Infrastructure.Data;

namespace HyperChatApp.Web.Endpoints.UserInfo;

public class UpdateUserInfoValidator : Validator<UpdateUserInfoRequest>
{
  public UpdateUserInfoValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(ModelConstants.NameLength);
  }
}
