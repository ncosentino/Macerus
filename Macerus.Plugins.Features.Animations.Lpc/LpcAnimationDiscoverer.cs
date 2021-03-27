using System;
using System.Collections.Generic;
using System.IO;

using Macerus.Plugins.Features.Animations.Api;

using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Animations.Lpc
{
    public sealed class LpcAnimationDiscoverer : ILpcAnimationDiscoverer
    {
        private readonly ILpcAnimationDiscovererSettings _lcpAnimationDiscovererSettings;
        private readonly ILpcSheetAnimationFactory _lpcSheetAnimationFactory;

        public LpcAnimationDiscoverer(
            ILpcAnimationDiscovererSettings lcpAnimationDiscovererSettings,
            ILpcSheetAnimationFactory lpcSheetAnimationFactory)
        {
            _lcpAnimationDiscovererSettings = lcpAnimationDiscovererSettings;
            _lpcSheetAnimationFactory = lpcSheetAnimationFactory;
        }

        public IEnumerable<ISpriteAnimation> CreateAnimations()
        {
            if (_lcpAnimationDiscovererSettings is NoneLpcAnimationDiscovererSettings)
            {
                yield break;
            }

            foreach (var file in Directory.GetFiles(_lcpAnimationDiscovererSettings.LcpUniversalPath))
            {
                var resourcePortionStart = file.LastIndexOf(
                    _lcpAnimationDiscovererSettings.RelativeLcpSpriteSheetPath,
                    StringComparison.OrdinalIgnoreCase);
                var resourcePortion = file.Substring(
                    resourcePortionStart,
                    file.Length - resourcePortionStart - Path.GetExtension(file).Length);
                var animations = _lpcSheetAnimationFactory.CreateForSheet(new StringIdentifier(resourcePortion));
                foreach (var animation in animations)
                {
                    yield return animation;
                }
            }
        }
    }
}
