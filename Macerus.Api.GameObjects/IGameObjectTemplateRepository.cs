using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectTemplateRepository
    {
        IEnumerable<IGameObject> GetTemplates(IFilterContext filterContext);
    }
}