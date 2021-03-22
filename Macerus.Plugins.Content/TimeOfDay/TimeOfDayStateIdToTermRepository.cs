using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;
using ProjectXyz.Plugins.Features.StateEnchantments.Shared;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.TimeOfDay
{
    public sealed class TimeOfDayStateIdToTermRepository : IDiscoverableStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield return new StateIdToTermMapping(
                new StringIdentifier("TimeOfDay"),
                new TermMapping(
                    new[]
                    {
                        new KeyValuePair<IIdentifier, string>(
                            TimesOfDay.Dawn,
                            "tod_dawn"),
                        new KeyValuePair<IIdentifier, string>(
                            TimesOfDay.Day,
                            "tod_day"),
                        new KeyValuePair<IIdentifier, string>(
                            TimesOfDay.Dusk,
                            "tod_dusk"),
                        new KeyValuePair<IIdentifier, string>(
                            TimesOfDay.Night,
                            "tod_night"),
                    }));
        }
    }
}
