using System.Collections.Generic;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Animations.Lpc
{
    public interface ILpcSheetAnimationFactory
    {
        IEnumerable<ISpriteAnimation> CreateForSheet(IIdentifier spriteSheetResourceId);
    }
}