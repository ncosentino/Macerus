using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors.Filtering
{
    public interface IFilterContextAmenity
    {
        IIdentifier GetGameObjectTypeIdFromContext(IFilterContext filterContext);

        IIdentifier GetGameObjectTemplateIdFromContext(IFilterContext filterContext);

        IFilterContext ExtendWithGameObjectIdFilter(
            IFilterContext filterContext,
            IIdentifier id);

        IFilterContext ExtendWithGameObjectTypeIdFilter(
            IFilterContext filterContext,
            IIdentifier templateId);

        IFilterContext ExtendWithGameObjectTemplateIdFilter(
            IFilterContext filterContext,
            IIdentifier templateId);
    }
}