using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasAffixType : 
        BaseBehavior,
        IHasAffixType
    {
        public HasAffixType(IIdentifier affixTypeId)
        {
            AffixTypeId = affixTypeId;
        }

        public IIdentifier AffixTypeId { get; }
    }
}