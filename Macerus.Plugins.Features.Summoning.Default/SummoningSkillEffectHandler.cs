using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningSkillEffectHandler : IDiscoverableSkillEffectHandler
    {
        private readonly ILogger _logger;
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly ISkillTargetingAmenity _skillTargetingAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;

        public SummoningSkillEffectHandler(
            IEnchantmentLoader enchantmentLoader,
            ISkillTargetingAmenity skillTargetingAmenity,
            IGameObjectFactory gameObjectFactory,
            ILogger logger)
        {
            _enchantmentLoader = enchantmentLoader;
            _skillTargetingAmenity = skillTargetingAmenity;
            _gameObjectFactory = gameObjectFactory;
            _logger = logger;
        }

        public int? Priority { get; } = null;

        public async Task<IGameObject> HandleAsync(
            IGameObject user,
            IGameObject skill,
            IGameObject skillEffect)
        {
            if (!skillEffect.TryGetFirst<ISummoningSkillEffectBehavior>(out var summoningSkillEffectBehavior))
            {
                return skillEffect;
            }

            var summoningEnchantment = _enchantmentLoader
                .LoadForEnchantmenDefinitionIds(new[]
                {
                    summoningSkillEffectBehavior.SummonEnchantmentDefinitionId
                })
                .Single();

            var targets = _skillTargetingAmenity.FindTargetLocationsForSkillEffect(
                user,
                skillEffect);
            var summoningEnchantmentWithTargets = _gameObjectFactory.Create(
                summoningEnchantment
                .Behaviors
                .Concat(new[] { new SummonTargetLocationBehavior(targets.Item2) }));

            var targetEnchantmentsBehavior = user.GetOnly<IHasEnchantmentsBehavior>();
            await targetEnchantmentsBehavior
                .AddEnchantmentsAsync(summoningEnchantmentWithTargets)
                .ConfigureAwait(false);
            _logger.Debug($"{user} enchanted self using summoning skill effect '{skillEffect}'.");


            return skillEffect;
        }
    }
}
