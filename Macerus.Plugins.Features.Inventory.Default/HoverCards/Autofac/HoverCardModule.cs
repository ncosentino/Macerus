using System;

using Autofac;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards.Autofac
{
    public sealed class HoverCardModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<BehaviorsToHoverCardPartViewModelConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HoverCardPartViewConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NameHoverCardPartConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneHoverCardViewFactory>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IHoverCardViewFactory))
                .SingleInstance();
        }

        private sealed class NoneHoverCardViewFactory : IHoverCardViewFactory
        {
            public object Create(IHoverCardViewModel viewModel) =>
                throw new NotSupportedException(
                    $"If you would like hover card support, please implement an " +
                    $"'{typeof(IHoverCardViewFactory)}' in the view technology " +
                    $"of your choice.");
        }
    }
}