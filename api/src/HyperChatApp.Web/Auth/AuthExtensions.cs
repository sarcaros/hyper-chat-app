using System.Security.Claims;
using FastEndpoints.Security;

namespace HyperChatApp.Web.Auth;

public static class AuthExtensions
{
  public static int? GetInternalId(this ClaimsPrincipal user)
  {
    var id = user.ClaimValue("subint");

    return string.IsNullOrEmpty(id)
      ? null
      : Convert.ToInt32(id);
  }

  public static IServiceCollection AddJwtAuth(this IServiceCollection sc, string tokenSigningKey, string? keyIssuer = null)
  {
    var key = tokenSigningKey.PadRight((256 / 8), '\0');
    return sc.AddJWTBearerAuth(key, JWTBearer.TokenSigningStyle.Symmetric,
    bearer =>
    {
      bearer.TokenValidationParameters.ValidIssuer = keyIssuer;
    });
  }
}
