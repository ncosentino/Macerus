using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Skills
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
                .ToArray();
            foreach (var skill in allSkills.Cast<IHasBehaviors>())
            {
                Assert.True(
                    skill.Get<IHasEnchantmentsBehavior>().Count() == 1,
                    $"Expecting that skill '{skill}' has exactly one {typeof(IHasEnchantmentsBehavior)} behavior.");
            }
        }
    }
}
