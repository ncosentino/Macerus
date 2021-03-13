using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
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

        /// <summary>
        /// Create our new context by keeping information about attributes 
        /// from our caller, but acknowledging that any that were required
        /// are now fulfilled up until this point. we then cobine in the
        /// newly provided attributes from the drop table.
        /// </summary>
        /// <param name="sourceFilterContext">
        /// The source filter context.
        /// </param>
        /// <param name="filterAttributes">
        /// The filter attributes to merge.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="IFilterContext"/>.
        /// </returns>
        IFilterContext CreateSubContext(
            IFilterContext sourceFilterContext,
            IEnumerable<IFilterAttribute> filterAttributes);
    }
}