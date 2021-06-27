using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class DirectionalAnimationIdReplacement : IDiscoverableAnimationIdReplacement
    {
        private readonly IMacerusActorIdentifiers _actorIdentifiers;

        public DirectionalAnimationIdReplacement(IMacerusActorIdentifiers actorIdentifiers)
        {
            _actorIdentifiers = actorIdentifiers;
        }

        public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetReplacementsAsync(IReadOnlyDynamicAnimationBehavior dynamicAnimationBehavior)
        {
            if (!dynamicAnimationBehavior.Owner.TryGetFirst<IReadOnlyMovementBehavior>(out var movementBehavior))
            {
                return new KeyValuePair<string, string>[0];
            }

            var directionReplacementId = _actorIdentifiers.GetAnimationDirectionId(movementBehavior.Direction);
            var replacementPattern = directionReplacementId.ToString();

            return new[]
            {
                new KeyValuePair<string, string>(
                    _actorIdentifiers.AnimationDirectionPlaceholder.ToString(),
                    replacementPattern),
            };
        }
    }
}
