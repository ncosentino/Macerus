using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class AnimationIdReplacementFacade : IAnimationIdReplacementFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableAnimationIdReplacement>> _lazyReplacements;

        public AnimationIdReplacementFacade(Lazy<IEnumerable<IDiscoverableAnimationIdReplacement>> replacements)
        {
            _lazyReplacements = new Lazy<IReadOnlyCollection<IDiscoverableAnimationIdReplacement>>(() =>
                replacements.Value.ToArray());
        }

        public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetReplacementsAsync(IReadOnlyDynamicAnimationBehavior dynamicAnimationBehavior)
        {
            var tasks = _lazyReplacements
                .Value
                .Select(x => x.GetReplacementsAsync(dynamicAnimationBehavior))
                .ToArray();
            var results = await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
            // FIXME: this feels gross to copy all the collection content
            // again... async enumerables please.
            return results
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
