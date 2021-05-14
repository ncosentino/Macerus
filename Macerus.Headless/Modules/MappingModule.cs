using System;
using System.IO;

using Autofac;

using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Headless
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MappingAssetPaths>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapResourceLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class MappingAssetPaths : IMappingAssetPaths
    {
        private readonly Lazy<DirectoryInfo> _lazyResourceRoot;
        private readonly Lazy<DirectoryInfo> _lazyMapsRoot;

        public MappingAssetPaths()
        {
            _lazyResourceRoot =
               new Lazy<DirectoryInfo>(() =>
               {
                   return new DirectoryInfo(@"..\..\..\Macerus\bin\debug\net46");
               });
            _lazyMapsRoot =
                new Lazy<DirectoryInfo>(() =>
                {
                    return new DirectoryInfo(Path.Combine(_lazyResourceRoot.Value.FullName, @"Mapping\Maps"));
                });
        }

        public string MapsRoot => _lazyMapsRoot.Value.FullName;

        public string ResourcesRoot => _lazyResourceRoot.Value.FullName;
    }

    public sealed class MapResourceLoader : IMapResourceLoader
    {
        private readonly IMappingAssetPaths _mappingAssetPaths;

        public MapResourceLoader(IMappingAssetPaths mappingAssetPaths)
        {
            _mappingAssetPaths = mappingAssetPaths;
        }

        public Stream LoadStream(string pathToResource)
        {
            var fullPath = Path.Combine(
                _mappingAssetPaths.ResourcesRoot,
                $"{pathToResource}.txt");
            if (!File.Exists(fullPath))
            {
                return null;
            }

            return File.OpenRead(fullPath);
        }
    }
}
