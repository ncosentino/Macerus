using Autofac;

using Macerus.Plugins.Features.Mapping.TiledNet;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework.Collections;

using Tiled.Net.Maps;
using Tiled.Net.Tmx.Xml;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet.Autofac
{
    public sealed class TiledMapsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x => new Cache<IIdentifier, ITiledMap>(5))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TilesetSpriteResourceResolver>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CachingTiledMapLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetToMapConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetGameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<MapResourceIdConverter>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<TiledNetMapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<XmlTmxMapParser>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
