using System.Collections.Generic;

using Macerus.Plugins.Features.Animations.Api;

namespace Macerus.Plugins.Features.Animations.Lpc
{
    public interface ILpcAnimationDiscoverer
    {
        IEnumerable<ISpriteAnimation> CreateAnimations();
    }
}