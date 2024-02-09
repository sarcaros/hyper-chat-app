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

builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);
});

builder.Services.AddSignalR();

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

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUI(x => x.EnableTryItOutByDefault());

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
