using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors.Filtering
{
    public interface IFilterContextAmenity
    {
        IFilterContext GetContext();

        IFilterContext CopyWithAdditionalAttributes(
            IFilterContext filterContext,
            IEnumerable<IFilterAttribute> additionalFilterAttributes);

        IFilterContext CopyWithRange(
            IFilterContext filterContext,
            int minimumCount,
            int maximumCount);

        IFilterContext CreateFilterContextForSingle(
            params IFilterAttribute[] attributes);

        IFilterContext CreateFilterContextForSingle(
            IEnumerable<IFilterAttribute> attributes);

        IFilterContext CreateFilterContextForAnyAmount(
            params IFilterAttribute[] attributes);

        IFilterContext CreateFilterContextForAnyAmount(
            IEnumerable<IFilterAttribute> attributes);

        IFilterContext CreateFilterContext(
            int minimum,
            int maximum,
            params IFilterAttribute[] attributes);

        IFilterContext CreateFilterContext(
            int minimum,
            int maximum,
            IEnumerable<IFilterAttribute> attributes);

        IFilterContext CreateNoneFilterContext();

        IIdentifier GetGameObjectTypeIdFromContext(
            IFilterContext filterContext);

        IIdentifier GetGameObjectTemplateIdFromContext(
            IFilterContext filterContext);

        IFilterContext ExtendWithGameObjectIdFilter(
            IFilterContext filterContext,
            IIdentifier id);

        IFilterContext ExtendWithGameObjectTypeIdFilter(
            IFilterContext filterContext,
            IIdentifier templateId);

        IFilterContext ExtendWithGameObjectTemplateIdFilter(
            IFilterContext filterContext,
            IIdentifier templateId);

        IFilterContext ExtendWithSupported(
            IFilterContext filterContext,
            IEnumerable<IFilterAttribute> filterContextToExtendWith);

        /// <summary>
        /// Create our new context by keeping information about attributes 
        /// from our caller, but acknowledging that any that were required
        /// are now fulfilled up until this point. We then combine in the
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

        IFilterContext CreateRequiredContextForSet(
            IFilterContext sourceFilterContext,
            IEnumerable<IHasFilterAttributes> setOfFilterAttributes);

        IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            IIdentifier value);

        IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            string value);

        IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            bool value);

        IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            params IIdentifier[] value);

        IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            IEnumerable<IIdentifier> value);

        IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            params string[] value);

        IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            IEnumerable<string> value);

        IFilterAttribute CreateSupportedAttributeForAll(
            IIdentifier id,
            params IIdentifier[] value);

        IFilterAttribute CreateSupportedAttributeForAll(
            IIdentifier id,
            IEnumerable<IIdentifier> value);

        IFilterAttribute CreateSupportedAlwaysMatchingAttribute(IIdentifier id);

        IFilterAttribute CreateRequiredAttribute(
            IIdentifier id,
            IIdentifier value);

        IFilterAttribute CreateRequiredAttribute(
            IIdentifier id,
            string value);

        IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            params string[] value);

        IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            IEnumerable<string> value);

        IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            IEnumerable<IIdentifier> value);
    }
}