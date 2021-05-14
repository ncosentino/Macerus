using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectTemplateRepository
    {
        IEnumerable<IGameObject> GetTemplates(IFilterContext filterContext);
    }
}