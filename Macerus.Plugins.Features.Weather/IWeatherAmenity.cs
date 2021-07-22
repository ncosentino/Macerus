using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather;

namespace Macerus.Plugins.Features.Weather
{
    public interface IWeatherAmenity
    {
        IWeatherTable GetWeatherTableById(IIdentifier weatherTableId);
    }
}