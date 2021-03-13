using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> Load(IFilterContext filterContext);

        IEnumerable<IGameObject> CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties);
    }
}