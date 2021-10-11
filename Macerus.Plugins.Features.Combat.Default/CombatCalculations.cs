using System.Collections.Generic;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatCalculations : ICombatCalculations
    {
        private readonly IStatCalculationService _statCalculationService;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;

        public CombatCalculations(
            IStatCalculationService statCalculationService,
            ICombatStatIdentifiers combatStatIdentifiers)
        {
            CalculateActorIncrementValue = GetActorIncrementValue;
            CalculateActorRequiredTargetValuePerTurn = GetActorRequiredTargetValuePerTurn;
            
            _statCalculationService = statCalculationService;
            _combatStatIdentifiers = combatStatIdentifiers;
        }

        public CombatCalculation<double> CalculateActorIncrementValue { get; }

        public CombatCalculation<double> CalculateActorRequiredTargetValuePerTurn { get; }

        private double GetActorIncrementValue(
            IFilterContext filterContext,
            IReadOnlyCollection<IGameObject> actors,
            IGameObject actor)
        {
            var speed = _statCalculationService.GetStatValue(
                actor,
                _combatStatIdentifiers.TurnSpeedStatId);
            return speed;
        }

        private double GetActorRequiredTargetValuePerTurn(
            IFilterContext filterContext,
            IReadOnlyCollection<IGameObject> actors,
            IGameObject actor)
        {
            // FIXME: decide if we want to scale this somehow
            return 100d;
        }
    }
}
