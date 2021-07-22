using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default;
using ProjectXyz.Plugins.Features.TimeOfDay;

namespace Macerus.Plugins.Content.TimeOfDay
{
    public sealed class TimeOfDayStateIdToTermRepository : IDiscoverableStateIdToTermRepository
    {
        private readonly ITimeOfDayIdentifiers _timeOfDayIdentifiers;

        public TimeOfDayStateIdToTermRepository(ITimeOfDayIdentifiers timeOfDayIdentifiers)
        {
            _timeOfDayIdentifiers = timeOfDayIdentifiers;
        }

        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield return new StateIdToTermMapping(
                _timeOfDayIdentifiers.TimeOfDayStateTypeId,
                new TermMapping(
                    new[]
                    {
                        new KeyValuePair<IIdentifier, string>(
                            _timeOfDayIdentifiers.CyclePercentStateId,
                            "tod_cycle_percent"),
                        // FIXME: do we want a boolean (1 or 0) expression
                        // term for the actual times of day? how would we wire that up?
                        //new KeyValuePair<IIdentifier, string>(
                        //    TimesOfDay.Dawn,
                        //    "tod_dawn"),
                        //new KeyValuePair<IIdentifier, string>(
                        //    TimesOfDay.Day,
                        //    "tod_day"),
                        //new KeyValuePair<IIdentifier, string>(
                        //    TimesOfDay.Dusk,
                        //    "tod_dusk"),
                        //new KeyValuePair<IIdentifier, string>(
                        //    TimesOfDay.Night,
                        //    "tod_night"),
                    }));
        }
    }
}
