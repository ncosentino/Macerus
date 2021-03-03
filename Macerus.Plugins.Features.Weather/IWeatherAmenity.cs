using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Macerus.Plugins.Features.Weather
{
    public interface IWeatherAmenity
    {
        IWeatherTable GetWeatherTableById(IIdentifier weatherTableId);
    }
}