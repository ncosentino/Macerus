using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherModifiers : IWeatherModifiers
    {
        public double GetMaximumDuration(IIdentifier weatherId, double baseMaximumDuration)
        {
            // FIXME: use existing game objects to calculate their effect on this
            return baseMaximumDuration;
        }

        public double GetMinimumDuration(IIdentifier weatherId, double baseMinimumDuration)
        {
            // FIXME: use existing game objects to calculate their effect on this
            return baseMinimumDuration;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights)
        {
            // FIXME: use existing game objects to calculate their effect on this
            return weatherWeights;
        }
    }
}
