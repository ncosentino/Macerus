using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatAIFactory : ICombatAIFactory
    {
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly IMapProvider _mapProvider;
        private readonly ILogger _logger;

        public CombatAIFactory(
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            ICombatTeamIdentifiers combatTeamIdentifiers,
            ICombatStatIdentifiers combatStatIdentifiers,
            IMacerusActorIdentifiers actorIdentifiers,
            IMapProvider mapProvider,
            ILogger logger)
        {
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _combatTeamIdentifiers = combatTeamIdentifiers;
            _combatStatIdentifiers = combatStatIdentifiers;
            _actorIdentifiers = actorIdentifiers;
            _mapProvider = mapProvider;
            _logger = logger;
        }

        public ICombatAI Create()
        {
            // FIXME: this should be able to provide different combat AI
            var combatAI = new PrimitiveAttackCombatAI(
                _statCalculationServiceAmenity,
                _skillUsage,
                _skillHandlerFacade,
                _combatTeamIdentifiers,
                _combatStatIdentifiers,
                _actorIdentifiers,
                _mapProvider,
                _logger);
            return combatAI;
        }
    }
}
