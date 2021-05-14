using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class CreatedFromTemplateBehavior :
        BaseBehavior,
        ICreatedFromTemplateBehavior
    {
        public CreatedFromTemplateBehavior(IIdentifier templateId)
        {
            TemplateId = templateId;
        }

        public IIdentifier TemplateId { get; }
    }
}