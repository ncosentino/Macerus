using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Api.Behaviors
{
    public interface ICreatedFromTemplateBehavior : IBehavior
    {
        IIdentifier TemplateId { get; }
    }
}