using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Api.Behaviors.Filtering;
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

        [Fact(Skip = "FIXME: we have no map repos... we need map repos.)")]
        private void StartEncounter_XX_XX()
        {
            _testAmenities.UsingCleanObjectManager(() =>
            {
                var filterContext = _filterContextProvider.GetContext();
                _encounterManager.StartEncounter(
                    filterContext,
                    new StringIdentifier("test-encounter"));
            });
        }
    }
}
