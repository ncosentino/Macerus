using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

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
                   return new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
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

        public async Task<Stream> LoadStreamAsync(string pathToResource)
        {
            var fullPath = Path.Combine(
                _mappingAssetPaths.ResourcesRoot,
                $"{pathToResource}.json");
            if (!File.Exists(fullPath))
            {
                return null;
            }

            return File.OpenRead(fullPath);
        }
    }
}
