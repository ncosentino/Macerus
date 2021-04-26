
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
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var filterContext = _filterContextProvider.GetContext();
                _encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));

                // FIXME: needs assertions
            });
        }
    }
}
