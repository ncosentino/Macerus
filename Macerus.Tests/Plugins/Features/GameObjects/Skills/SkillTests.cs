using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillTests
    {
        private static readonly MacerusContainer _container;

        static SkillTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void SkillRepository_IntegrityTest()
        {
            var skillRepository = _container.Resolve<ISkillRepository>();

            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var skillFilterContext = filterContextFactory.CreateFilterContextForAnyAmount(
                new IFilterAttribute[]
                {
                });

            var allSkills = skillRepository
                .GetSkills(skillFilterContext)
                .Where(x => x.Has<IHasEnchantmentsBehavior>())
                .ToArray();
            foreach (var skill in allSkills.Cast<IGameObject>())
            {
                Assert.True(
                    skill.Get<IHasEnchantmentsBehavior>().Count() == 1,
                    $"Expecting that skill '{skill}' has exactly one " +
                    $"{typeof(IHasEnchantmentsBehavior)} behavior.");
                foreach (var enchantment in skill
                    .GetOnly<IHasReadOnlyEnchantmentsBehavior>()
                    .Enchantments)
                {
                    Assert.True(
                        enchantment.Get<IEnchantmentTargetBehavior>().Count() == 1,
                        $"Expecting that skill '{enchantment}' has exactly one " +
                        $"{typeof(IEnchantmentTargetBehavior)} behavior.");
                }
            }
        }
    }
}
