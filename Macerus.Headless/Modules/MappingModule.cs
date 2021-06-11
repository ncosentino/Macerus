using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

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
            builder
                .RegisterType<MapTraversableHighlighter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class MapTraversableHighlighter : IMapTraversableHighlighter
    {
        public void SetTraversableTiles(IEnumerable<Vector2> traversableTiles)
        {
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
                $"{pathToResource}.json");
            if (!File.Exists(fullPath))
            {
                return null;
            }

            return File.OpenRead(fullPath);
        }
    }
}
