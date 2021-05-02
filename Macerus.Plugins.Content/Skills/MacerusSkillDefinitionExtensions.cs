using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Skills
{
    public interface ISkillDefinitionExecutor
    {
        bool IsParallel { get; }

        IEnumerable<ISkillDefinition> SkillDefinitions { get; }
    }

    public sealed class SkillDefinitionExecutor : ISkillDefinitionExecutor
    {
        public SkillDefinitionExecutor(
            bool isParallel,
            IEnumerable<ISkillDefinition> skillDefinitions)
        {
            IsParallel = isParallel;
            SkillDefinitions = skillDefinitions;
        }

        public bool IsParallel { get; }

        public IEnumerable<ISkillDefinition> SkillDefinitions { get; }
    }

    public static class MacerusExecuteSkills
    {
        public static ISkillDefinitionExecutor InParallel(
            params ISkillDefinition[] skillDefinitions)
        {
            return new SkillDefinitionExecutor(true, skillDefinitions);
        }

        public static ISkillDefinitionExecutor InSequence(
            params ISkillDefinition[] skillDefinitions)
        {
            return new SkillDefinitionExecutor(false, skillDefinitions);
        }
    }

    public static class MacerusSkillDefinitionExtensionss
    {
        public static ISkillDefinition WithDisplayIcon(
            this ISkillDefinition oldSkillDefinition,
            string iconResourcePath)
        {
            var displayIconGenerator = new DisplayIconGeneratorComponent(iconResourcePath);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(displayIconGenerator));

            return skillDefinition;
        }

        public static ISkillDefinition WithDisplayName(
            this ISkillDefinition oldSkillDefinition,
            string displayName)
        {
            var displayNameGenerator = new DisplayNameComponentGenerator(displayName);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(displayNameGenerator));

            return skillDefinition;
        }

        public static ISkillDefinition WithSupportedAttributes(
            this ISkillDefinition oldSkillDefinition,
            params IFilterAttribute[] SupportedAttributes)
        {
            var newSupportedAttributes = oldSkillDefinition
                .SupportedAttributes
                .Concat(SupportedAttributes);

            var skillDefinition = new SkillDefinition(
                newSupportedAttributes,
                oldSkillDefinition.FilterComponents);

            return skillDefinition;
        }

        public static ISkillDefinition CanBeUsedInCombat(
            this ISkillDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new UseInCombatGeneratorComponent()));

            return skillDefinition;
        }

        public static ISkillDefinition CanBeUsedOutOfCombat(
            this ISkillDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new UseOutOfCombatGeneratorComponent()));

            return skillDefinition;
        }

        public static ISkillDefinition Enchant(
            this ISkillDefinition oldSkillDefinition,
            params string[] enchantments)
        {
            var enchantGeneratorComponent = new EnchantTargetsGeneratorComponent(enchantments);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(enchantGeneratorComponent));

            return skillDefinition;
        }

        public static ISkillDefinition EnchantPassive(
            this ISkillDefinition oldSkillDefinition,
            params string[] enchantments)
        {
            var enchantGeneratorComponent = new PassiveEnchantmentGeneratorComponent(enchantments);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(enchantGeneratorComponent));

            return skillDefinition;
        }

        public static ISkillDefinition InflictDamage(
            this ISkillDefinition oldSkillDefinition)
        {
            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(new InflictDamageGeneratorComponent()));

            return skillDefinition;
        }

        public static IEnumerable<ISkillDefinition> IsACombinationOf(
            this ISkillDefinition oldSkillDefinition,
            params ISkillDefinitionExecutor[] skillDefinitionExecutors)
        {
            var definitionsToSurface = new List<ISkillDefinition>();
            foreach (var definition in skillDefinitionExecutors.SelectMany(x => x.SkillDefinitions))
            {
                var identifierComponent = definition
                    .FilterComponents
                    .SingleOrDefault(x => x is SkillIdentifierGeneratorComponent) as SkillIdentifierGeneratorComponent;

                if (identifierComponent == null || !Guid.TryParse(identifierComponent.SkillDefinitionId.ToString(), out var _))
                {
                    continue;
                }

                definitionsToSurface.Add(definition);
            }

            var executorComponents = skillDefinitionExecutors
                .Select(x => x.IsParallel
                    ? new ParallelSkillExecutorGeneratorComponent(
                        x.SkillDefinitions.Select(y => PullId(y)).Where(y => y != null)) as ISkillExecutorGeneratorComponent
                    : new SequentialSkillExecutorGeneratorComponent(
                        x.SkillDefinitions.Select(y => PullId(y)).Where(y => y != null)) as ISkillExecutorGeneratorComponent)
                .ToArray();

            var combinationComponent = new SkillCombinationGeneratorComponent(executorComponents);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(combinationComponent));

            return definitionsToSurface
                .AppendSingle(skillDefinition);
        }

        public static ISkillDefinition WithResourceRequirement(
            this ISkillDefinition oldSkillDefinition,
            int resourceId,
            double cost)
        {
            var existingCosts = oldSkillDefinition
                .FilterComponents
                .FirstOrDefault(x => x is StaticResourceRequirementsGeneratorComponent)
                as StaticResourceRequirementsGeneratorComponent;
            if (existingCosts == null)
            {
                var requirementsGenerator = new StaticResourceRequirementsGeneratorComponent(
                    new IntIdentifier(resourceId),
                    cost);

                return new SkillDefinition(
                    oldSkillDefinition.SupportedAttributes,
                    oldSkillDefinition
                        .FilterComponents
                        .AppendSingle(requirementsGenerator));
            }

            var oldSkillDefinitionWithoutCosts = oldSkillDefinition
                .FilterComponents
                .Except(new[] { existingCosts });

            var allCosts = existingCosts
                .StaticResourceRequirements
                .ToDictionary(
                    x => x.Key,
                    x => x.Value);

            allCosts.Add(new IntIdentifier(resourceId), cost);

            var updatedRequirementsGenerator = new StaticResourceRequirementsGeneratorComponent(
                new IntIdentifier(resourceId),
                cost);

            return new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinitionWithoutCosts
                    .AppendSingle(updatedRequirementsGenerator));
        }

        public static ISkillDefinition AffectsTeams(
            this ISkillDefinition oldSkillDefinition,
            params int[] affectedTeams)
        {
            var skillTargetTeamComponent = new SkillTargetCombatTeamGeneratorComponent(
                affectedTeams);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(skillTargetTeamComponent));

            return skillDefinition;
        }

        public static ISkillDefinition StartsAtOffsetFromUser(
            this ISkillDefinition oldSkillDefinition,
            int offsetFromUserX,
            int offsetFromUserY)
        {
            var skillOffsetFromUserComponent = new SkillTargetOriginGeneratorComponent(
                offsetFromUserX,
                offsetFromUserY);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(skillOffsetFromUserComponent));

            return skillDefinition;
        }

        public static ISkillDefinition TargetsPattern(
            this ISkillDefinition oldSkillDefinition,
            params Tuple<int, int>[] locations)
        {
            var skillPatternComponent = new SkillTargetPatternGeneratorComponent(locations);

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(skillPatternComponent));

            return skillDefinition;
        }

        public static IEnumerable<ISkillDefinition> End(
            this ISkillDefinition oldSkillDefinition)
        {
            var id = PullId(oldSkillDefinition);
            if (id == null)
            {
                return new[] { oldSkillDefinition };
            }

            var combinationComponent = new SkillCombinationGeneratorComponent(
                new SequentialSkillExecutorGeneratorComponent(
                    new[] { id }));

            var skillDefinition = new SkillDefinition(
                oldSkillDefinition.SupportedAttributes,
                oldSkillDefinition
                    .FilterComponents
                    .AppendSingle(combinationComponent));


            return new[] { skillDefinition };
        }

        public static IEnumerable<ISkillDefinition> End(
            this IEnumerable<ISkillDefinition> oldSkillDefinitions)
        {
            return oldSkillDefinitions;
        }

        private static IIdentifier PullId(ISkillDefinition def)
        {
            var identifierComponent = def
                .FilterComponents
                .SingleOrDefault(x => x is SkillIdentifierGeneratorComponent) as SkillIdentifierGeneratorComponent;

            if (identifierComponent == null)
            {
                return null;
            }

            return identifierComponent.SkillDefinitionId;
        }
    }
}
