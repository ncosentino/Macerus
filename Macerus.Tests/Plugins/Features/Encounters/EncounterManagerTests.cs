using System;
using System.IO;

using Autofac;

using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Mapping.TiledNet;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Encounters
{
    public sealed class EncounterManagerTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IEncounterManager _encounterManager;
        private static readonly IFilterContextProvider _filterContextProvider;

        static EncounterManagerTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _encounterManager = _container.Resolve<IEncounterManager>();
            _filterContextProvider = _container.Resolve<IFilterContextProvider>();
        }

        [Fact]
        private void StartEncounter_TestEncounter_FIXMENeedsAssertions()
        {
            _testAmenities.UsingCleanObjectManager(() =>
            {
                var filterContext = _filterContextProvider.GetContext();
                _encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                // FIXME: needs assertions
            });
        }
    }

    public sealed class MapModule : SingleRegistrationModule
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

        public sealed class MappingAssetPaths : IMappingAssetPaths
        {
            private readonly Lazy<DirectoryInfo> _lazyResourceRoot;
            private readonly Lazy<DirectoryInfo> _lazyMapsRoot;

            public MappingAssetPaths()
            {
                _lazyResourceRoot =
                   new Lazy<DirectoryInfo>(() =>
                   {
                       return new DirectoryInfo(@"..\..\..\..\Macerus\bin\debug\net46");
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

        public sealed class MapResourceLoader : ITiledMapResourceLoader
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
                return File.OpenRead(fullPath);
            }
        }
    }
}
