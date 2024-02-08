using Ardalis.SharedKernel;
using Autofac;
using HyperChatApp.Infrastructure.Auth;
using HyperChatApp.Infrastructure.Data;
using MediatR;
using MediatR.Pipeline;

namespace HyperChatApp.Infrastructure;

public class InfrastructureModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterEf(builder);
    RegisterServices(builder);
  }
  private void RegisterEf(ContainerBuilder builder)
  {
    builder.RegisterGeneric(typeof(EfRepository<>))
      .AsImplementedInterfaces()
      .InstancePerLifetimeScope();
  }

  private void RegisterServices(ContainerBuilder builder)
  {
    builder
      .RegisterType<UserAuthService>()
      .As<IUserAuthService>()
      .SingleInstance();
  }
}
