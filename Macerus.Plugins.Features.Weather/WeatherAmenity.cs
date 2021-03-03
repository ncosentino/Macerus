using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherAmenity : IWeatherAmenity
    {
        private readonly IWeatherIdentifiers _weatherIdentifiers;
        private readonly IWeatherTableRepositoryFacade _weatherTableRepositoryFacade;
        private readonly IFilterContextFactory _filterContextFactory;

        public WeatherAmenity(
            IWeatherIdentifiers weatherIdentifiers,
            IWeatherTableRepositoryFacade weatherTableRepositoryFacade,
            IFilterContextFactory filterContextFactory)
        {
            _weatherIdentifiers = weatherIdentifiers;
            _weatherTableRepositoryFacade = weatherTableRepositoryFacade;
            _filterContextFactory = filterContextFactory;
        }

        public IWeatherTable GetWeatherTableById(IIdentifier weatherTableId)
        {
            var weatherTables = _weatherTableRepositoryFacade
                .GetWeatherTables(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                {
                    new FilterAttribute(
                        _weatherIdentifiers.WeatherIdentifier,
                        new IdentifierFilterAttributeValue(weatherTableId),
                        true),
                }))
                .ToArray();
            Contract.Requires(
                weatherTables.Length <= 1,
                $"Expecting 0 or 1 weather tables matching ID '{weatherTableId}' " +
                $"but there were {weatherTables.Length}.");
            return weatherTables.FirstOrDefault();
        }
    }
}
