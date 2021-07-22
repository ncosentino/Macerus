using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherIdentifiers : IWeatherIdentifiers
    {
        public IIdentifier FilterContextWeatherIdentifier { get; } = new StringIdentifier("weather");

        public IIdentifier WeatherIdentifier { get; } = new StringIdentifier("id");

        public IIdentifier WeatherStateTypeId { get; } = new StringIdentifier("weather");

        public IIdentifier KindOfWeatherStateId { get; } = new StringIdentifier("weather-kind");
    }
}
