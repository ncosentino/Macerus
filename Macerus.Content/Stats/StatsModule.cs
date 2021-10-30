using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();
                    var actorIdentifiers = c.Resolve<IMacerusActorIdentifiers>();

                    // FIXME: these are all temporary just for testing so we
                    // don't need to keep updating the database while we are
                    // iterating on features
                    var mapping = new Dictionary<IIdentifier, string>()
                    {
                        // summoning
                        [new StringIdentifier("maximum_summon_skeletons")] = "SUMMON_SKELETONS_MAX",
                    };
                    var statDefinitionToTermRepository = new InMemoryStatDefinitionToTermMappingRepository(mapping);
                    return statDefinitionToTermRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}