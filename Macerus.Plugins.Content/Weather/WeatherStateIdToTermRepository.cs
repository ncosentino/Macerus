using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;
using ProjectXyz.Plugins.Features.StateEnchantments.Shared;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Weather
{
    public sealed class WeatherStateIdToTermRepository : IDiscoverableStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield return new StateIdToTermMapping(
                new StringIdentifier("Weather"),
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
