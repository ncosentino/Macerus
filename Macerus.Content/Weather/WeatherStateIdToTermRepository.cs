using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default;
using ProjectXyz.Plugins.Features.Weather;

namespace Macerus.Content.Weather
{
    public sealed class WeatherStateIdToTermRepository : IDiscoverableStateIdToTermRepository
    {
        private readonly IWeatherIdentifiers _weatherIdentifiers;

        public WeatherStateIdToTermRepository(IWeatherIdentifiers weatherIdentifiers)
        {
            _weatherIdentifiers = weatherIdentifiers;
        }

        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield return new StateIdToTermMapping(
                _weatherIdentifiers.WeatherStateTypeId,
                new TermMapping(
                    new KeyValuePair<IIdentifier, string>[]
                    {
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Clear,
                            "weather_clear"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Rain,
                            "weather_rain"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Snow,
                            "weather_snow"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Overcast,
                            "weather_overcast"),
                    }));
        }
    }
}
