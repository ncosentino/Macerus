
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather
{
    public static class WeatherIds
    {
        public static IIdentifier Clear { get; } = new StringIdentifier("clear");

        public static IIdentifier Rain { get; } = new StringIdentifier("rain");

        public static IIdentifier Snow { get; } = new StringIdentifier("snow");

        public static IIdentifier Overcast { get; } = new StringIdentifier("overcast");
    }
}
