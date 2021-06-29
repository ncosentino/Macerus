using System.Collections.Generic;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IAnimationIdReplacement
    {
        // FIXME: would be great to use async enumerables but... thanks Unity
        Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetReplacementsAsync(IReadOnlyDynamicAnimationBehavior dynamicAnimationBehavior);
    }
}
