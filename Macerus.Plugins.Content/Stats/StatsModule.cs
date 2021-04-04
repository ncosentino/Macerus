using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();

                    // FIXME: these are all temporary just for testing so we
                    // don't need to keep updating the database while we are
                    // iterating on features
                    var mapping = new Dictionary<IIdentifier, string>()
                    {
                        [new StringIdentifier("speed")] = "SPEED",
                        [combatTeamIdentifiers.CombatTeamStatDefinitionId] = "COMBAT_TEAM",
                    };
                    var statDefinitionToTermRepository = new InMemoryStatDefinitionToTermMappingRepository(mapping);
                    return statDefinitionToTermRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}