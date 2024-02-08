using Ardalis.SharedKernel;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace HyperChatApp.UseCases;

public class UseCasesModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterMediatR(builder);
  }

  private void RegisterMediatR(ContainerBuilder builder)
  {
    builder
      .RegisterType<Mediator>()
      .As<IMediator>()
      .InstancePerLifetimeScope();

    builder
      .RegisterGeneric(typeof(LoggingBehavior<,>))
      .As(typeof(IPipelineBehavior<,>))
      .InstancePerLifetimeScope();

    builder
      .RegisterType<MediatRDomainEventDispatcher>()
      .As<IDomainEventDispatcher>()
      .InstancePerLifetimeScope();

    var mediatrOpenTypes = new[]
    {
      typeof(IRequestHandler<,>),
      typeof(IRequestExceptionHandler<,,>),
      typeof(IRequestExceptionAction<,>),
      typeof(INotificationHandler<>),
    };

    foreach (var mediatrOpenType in mediatrOpenTypes)
    {
      builder
        .RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(mediatrOpenType)
        .AsImplementedInterfaces();
    }
  }
}
