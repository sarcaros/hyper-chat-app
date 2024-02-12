using System.Security.Claims;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HyperChatApp.Web.Auth;

public static class AuthExtensions
{
  public static int GetInternalId(this ClaimsPrincipal? user)
  {
    var id = user?.ClaimValue("subint") ?? throw new InternalIdClaimNotFound();
    return Convert.ToInt32(id);
  }

  public static IServiceCollection AddJwtAuth(this IServiceCollection sc, string tokenSigningKey, string? keyIssuer = null)
  {
    var key = tokenSigningKey.PadRight((256 / 8), '\0');
    return sc.AddJWTBearerAuth(key, JWTBearer.TokenSigningStyle.Symmetric,
    bearer =>
    {
      bearer.TokenValidationParameters.ValidIssuer = keyIssuer;
      bearer.Events = new JwtBearerEvents
      {
        OnMessageReceived = ctx =>
        {
          var token = ctx.Request.Query["access_token"];
          var path = ctx.HttpContext.Request.Path;
          if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/hubs-chat"))
          {
            // Read the token out of the query string
            ctx.Token = token;
          }
          return Task.CompletedTask;
        },
      };
    });
  }

  public class InternalIdClaimNotFound : InvalidOperationException
  { }
}
