using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Skills;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillHandlerFacade : ISkillHandlerFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableSkillEffectHandler>> _lazySkillEffectHandlers;
        private readonly Lazy<IReadOnlyCollection<IDiscoverableSkillHandler>> _lazySkillHandlers;

        public SkillHandlerFacade(
            Lazy<IEnumerable<IDiscoverableSkillEffectHandler>> lazySkillEffectHandlers,
            Lazy<IEnumerable<IDiscoverableSkillHandler>> lazySkillHandlers)
        {
            _lazySkillEffectHandlers = new Lazy<IReadOnlyCollection<IDiscoverableSkillEffectHandler>>(() => lazySkillEffectHandlers
                .Value
                .OrderBy(x => x.Priority == null
                    ? int.MaxValue
                    : x.Priority.Value)
                .ToArray());
            _lazySkillHandlers = new Lazy<IReadOnlyCollection<IDiscoverableSkillHandler>>(() => lazySkillHandlers
                .Value
                .OrderBy(x => x.Priority == null
                    ? int.MaxValue
                    : x.Priority.Value)
                .ToArray());
        }

        public async Task HandleSkillAsync(
            IGameObject user,
            IGameObject skill)
        {
            var skillEffectBehavior = skill.GetOnly<ISkillEffectBehavior>();

            foreach (var executor in skillEffectBehavior.EffectExecutors)
            {
                foreach (var executorBehavior in executor.Get<ISkillEffectExecutorBehavior>())
                {
                    var effectsToExecute = executorBehavior.Effects;

                    if (executorBehavior is ISequentialSkillEffectExecutorBehavior)
                    {
                        foreach (var effect in effectsToExecute)
                        {
                            await HandleSkillEffectAsync(user, skill, effect).ConfigureAwait(false);
                        }

                        continue;
                    }

                    if (executorBehavior is IParallelSkillEffectExecutorBehavior)
                    {
                        var tasks = effectsToExecute.Select(effect => HandleSkillEffectAsync(user, skill, effect));
                        await Task
                            .WhenAll(tasks)
                            .ConfigureAwait(false);
                        continue;
                    }
                }
            }

            foreach (var handler in _lazySkillHandlers.Value)
            {
                await handler.HandleAsync(user, skill);
            }
        }

        private async Task HandleSkillEffectAsync(
            IGameObject user,
            IGameObject skill,
            IGameObject skillEffect)
        {
            var updatedSkillEffect = skillEffect;
            foreach (var handler in _lazySkillEffectHandlers.Value)
            {
                updatedSkillEffect = await handler
                    .HandleAsync(
                        user, 
                        skill,
                        updatedSkillEffect)
                    .ConfigureAwait(false);
            }
        }
    }
}
