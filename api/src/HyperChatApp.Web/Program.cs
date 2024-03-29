using Ardalis.GuardClauses;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using FastEndpoints.ApiExplorer;
using Serilog;

using HyperChatApp.Infrastructure.Data;
using HyperChatApp.Core;
using HyperChatApp.UseCases;
using HyperChatApp.Infrastructure;
using HyperChatApp.Web.Auth;
using HyperChatApp.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// use autofac as for the DI
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// use serilog for the logging
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.

// configure database connection
string? dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Guard.Against.Null(dbConnectionString);
builder.Services.AddDbContexts(dbConnectionString);

builder.Services.AddCors(options =>
{
  options.AddPolicy(
    name: "mycors",
    policy =>
    {
      policy//.WithOrigins("https://localhost:3000")
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                   .AllowCredentials();
    });
});

builder.Services
  .AddFastEndpoints()
  .AddFastEndpointsApiExplorer();

string? jwtSigningKey = builder.Configuration["Auth:SigningKey"];
string? jwtKeyIssuer = builder.Configuration["Auth:KeyIssuer"];

Guard.Against.Null(jwtSigningKey);
builder.Services
  .AddJwtAuth(jwtSigningKey, jwtKeyIssuer)
  .AddAuthorization();

builder.Services.SwaggerDocument(doc =>
{
  doc.ShortSchemaNames = true;
  doc.DocumentSettings = s =>
  {
    s.Title = "HyperChatApp API";
    s.Version = "v1";
  };
});

string? clerkApiKey = builder.Configuration["Auth:ClerkApiKey"];
Guard.Against.NullOrEmpty(clerkApiKey);

builder.Services.AddHttpClient("clerk",
c =>
{
  /*curl -XPATCH -H 'Authorization: Bearer CLERK_SECRET_KEY' -H "Content-type: application/json" -d '{
"public_metadata": {
  "role": "shopper"
}
}' 'https://api.clerk.com/v1/users/{user_id}/metadata'*/
  c.BaseAddress = new Uri("https://api.clerk.com/v1/");
  c.DefaultRequestHeaders.Add("Authorization", $"Bearer {clerkApiKey}");
});

builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);
});

var signalRBuilder = builder.Services.AddSignalR();

// register Autofac DI container
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
  builder.RegisterModule<CoreModule>();
  builder.RegisterModule<UseCasesModule>();
  builder.RegisterModule<InfrastructureModule>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseDefaultExceptionHandler();
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("mycors");

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUI(x =>
{
  x.EnableTryItOutByDefault();
});

app.MapHub<ChatHub>("/hubs-chat");

MigrateDatabase(app);

app.Run();

static void MigrateDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    context.EnsureCreated();
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}
