using System.Collections.Generic;

using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Macerus.Plugins.Features.Weather
{
    public sealed class WeatherBehaviorsProvider : IDiscoverableWeatherBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            // FIXME: should there be a translation here?
            var weatherId = baseBehaviors
                .GetOnly<IIdentifierBehavior>()
                .Id;
            yield return new HasPrefabResourceIdBehavior(weatherId);
        }
    }
}
