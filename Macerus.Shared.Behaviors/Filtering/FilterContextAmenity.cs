using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;

namespace Macerus.Shared.Behaviors.Filtering
{
    public sealed class FilterContextAmenity : IFilterContextAmenity
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;

        public FilterContextAmenity(
            IFilterContextFactory filterContextFactory,
            IFilterContextProvider filterContextProvider,
            IGameObjectIdentifiers gameObjectIdentifiers)
        {
            _filterContextFactory = filterContextFactory;
            _filterContextProvider = filterContextProvider;
            _gameObjectIdentifiers = gameObjectIdentifiers;
        }

        public IFilterContext GetContext() =>
            _filterContextProvider.GetContext();

        public IFilterContext CreateFilterContextForSingle(
            params IFilterAttribute[] attributes) =>
            _filterContextFactory.CreateFilterContextForSingle(attributes);

        public IFilterContext CreateFilterContextForSingle(
            IEnumerable<IFilterAttribute> attributes) =>
            _filterContextFactory.CreateFilterContextForSingle(attributes);

        public IFilterContext CreateFilterContextForAnyAmount(
            params IFilterAttribute[] attributes) =>
            _filterContextFactory.CreateFilterContextForAnyAmount(attributes);

        public IFilterContext CreateFilterContextForAnyAmount(
            IEnumerable<IFilterAttribute> attributes) =>
            _filterContextFactory.CreateFilterContextForAnyAmount(attributes);

        public IFilterContext CreateFilterContext(
            int minimum,
            int maximum,
            params IFilterAttribute[] attributes) =>
            _filterContextFactory.CreateContext(minimum, maximum, attributes);

        public IFilterContext CreateFilterContext(
            int minimum,
            int maximum,
            IEnumerable<IFilterAttribute> attributes) =>
            _filterContextFactory.CreateContext(minimum, maximum, attributes);

        public IFilterContext CreateNoneFilterContext() =>
            _filterContextFactory.CreateNoneFilterContext();

        public IFilterContext ExtendWithGameObjectIdFilter(
            IFilterContext filterContext,
            IIdentifier id)
        {
            var context = _filterContextFactory
                .CreateContext(
                filterContext,
                new FilterAttribute(
                    _gameObjectIdentifiers.FilterContextId,
                    new IdentifierFilterAttributeValue(id),
                    true));
            return context;
        }

        public IFilterContext ExtendWithGameObjectTypeIdFilter(
            IFilterContext filterContext,
            IIdentifier typeId)
        {
            var context = _filterContextFactory
                .CreateContext(
                filterContext,
                new FilterAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    new IdentifierFilterAttributeValue(typeId),
                    true));
            return context;
        }

        public IFilterContext ExtendWithGameObjectTemplateIdFilter(
            IFilterContext filterContext,
            IIdentifier templateId)
        {
            var context = _filterContextFactory.CreateContext(
                filterContext,
                new FilterAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new IdentifierFilterAttributeValue(templateId),
                    true));
            return context;
        }

        public IFilterContext ExtendWithSupported(
            IFilterContext filterContext,
            IEnumerable<IFilterAttribute> attributes)
        {
            var supportedAttributes = attributes
                .Select(x => x.Required
                    ? x.CopyWithRequired(false)
                    : x)
                .ToDictionary(x => x.Id, x => x);
            var context = _filterContextFactory.CreateContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
                    .Attributes
                    .Where(x => !supportedAttributes.ContainsKey(x.Id))
                    .Concat(supportedAttributes.Values));
            return context;
        }

        public IFilterContext CopyWithAdditionalAttributes(
            IFilterContext filterContext,
            IEnumerable<IFilterAttribute> additionalFilterAttributes)
        {
            var newContext = _filterContextFactory.CreateContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
                    .Attributes
                    .Concat(additionalFilterAttributes));
            return newContext;
        }

        public IFilterContext CopyWithRange(
            IFilterContext filterContext,
            int minimumCount,
            int maximumCount)
        {
            var newContext = _filterContextFactory.CreateContext(
                minimumCount,
                maximumCount,
                filterContext.Attributes);
            return newContext;
        }

        public IIdentifier GetGameObjectTypeIdFromContext(IFilterContext filterContext)
        {
            var attribute = filterContext
                .Attributes
                .FirstOrDefault(x => x.Id.Equals(_gameObjectIdentifiers.FilterContextTypeId));
            Contract.RequiresNotNull(
                attribute,
                $"Filter context was missing required attribute '{_gameObjectIdentifiers.FilterContextTypeId}'.");
            Contract.Requires(
                attribute.Value is IdentifierFilterAttributeValue,
                $"Filter context attribute value '{attribute.Value}' was not of " +
                $"type '{typeof(IdentifierFilterAttributeValue)}'.");
            var value = ((IdentifierFilterAttributeValue)attribute.Value).Value;
            return value;
        }

        public IIdentifier GetGameObjectTemplateIdFromContext(IFilterContext filterContext)
        {
            var attribute = filterContext
                .Attributes
                .FirstOrDefault(x => x.Id.Equals(_gameObjectIdentifiers.FilterContextTemplateId));
            Contract.RequiresNotNull(
                attribute,
                $"Filter context was missing required attribute '{_gameObjectIdentifiers.FilterContextTemplateId}'.");
            Contract.Requires(
                attribute.Value is IdentifierFilterAttributeValue,
                $"Filter context attribute value '{attribute.Value}' was not of " +
                $"type '{typeof(IdentifierFilterAttributeValue)}'.");
            var value = ((IdentifierFilterAttributeValue)attribute.Value).Value;
            return value;
        }

        /// <inheritdoc />
        public IFilterContext CreateSubContext(
            IFilterContext sourceFilterContext,
            IEnumerable<IFilterAttribute> filterAttributes)
        {
            var requiredAttributes = filterAttributes
                .Where(attr => attr.Required)
                .ToDictionary(x => x.Id, x => x);
            var subFilterContext = _filterContextFactory.CreateContext(
                sourceFilterContext.MinimumCount,
                sourceFilterContext.MaximumCount,
                sourceFilterContext
                    .Attributes
                    .Where(x => !requiredAttributes.ContainsKey(x.Id))
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(requiredAttributes.Values));
            return subFilterContext;
        }

        public IFilterContext CreateRequiredContextForSet(
            IFilterContext sourceFilterContext,
            IEnumerable<IHasFilterAttributes> setOfFilterAttributes)
        {
            var requiredAttributeIds = new HashSet<IIdentifier>(setOfFilterAttributes
                .SelectMany(x => x.SupportedAttributes)
                .Where(x => x.Required)
                .Select(x => x.Id)
                .Distinct());
            var newFilterContext = _filterContextFactory.CreateContext(
                sourceFilterContext.MinimumCount,
                sourceFilterContext.MaximumCount,
                sourceFilterContext
                    .Attributes
                    .Where(x => requiredAttributeIds.Contains(x.Id)));
            return newFilterContext;
        }

        public IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            IIdentifier value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new IdentifierFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            string value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new StringFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAttribute(
            IIdentifier id,
            bool value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new BooleanFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            params IIdentifier[] value) =>
            CreateSupportedAttributeForAny(
                id,
                (IEnumerable<IIdentifier>)value);

        public IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            IEnumerable<IIdentifier> value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new AnyIdentifierCollectionFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            params string[] value) =>
            CreateSupportedAttributeForAny(
                id,
                (IEnumerable<string>)value);

        public IFilterAttribute CreateSupportedAttributeForAny(
            IIdentifier id,
            IEnumerable<string> value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new AnyStringCollectionFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAttributeForAll(
            IIdentifier id,
            params IIdentifier[] value) =>
            CreateSupportedAttributeForAll(
                id,
                (IEnumerable<IIdentifier>)value);

        public IFilterAttribute CreateSupportedAttributeForAll(
            IIdentifier id,
            IEnumerable<IIdentifier> value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new AnyIdentifierCollectionFilterAttributeValue(value),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateSupportedAlwaysMatchingAttribute(IIdentifier id)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new TrueAttributeFilterValue(),
                false);
            return filterAttribute;
        }

        public IFilterAttribute CreateRequiredAttribute(
            IIdentifier id,
            IIdentifier value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new IdentifierFilterAttributeValue(value),
                true);
            return filterAttribute;
        }

        public IFilterAttribute CreateRequiredAttribute(
            IIdentifier id,
            string value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new StringFilterAttributeValue(value),
                true);
            return filterAttribute;
        }

        public IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            params string[] value) => CreateRequiredAttributeForAny(
                id,
                (IEnumerable<string>)value);

        public IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            IEnumerable<string> value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new AnyStringCollectionFilterAttributeValue(value),
                true);
            return filterAttribute;
        }

        public  IFilterAttribute CreateRequiredAttributeForAny(
            IIdentifier id,
            IEnumerable<IIdentifier> value)
        {
            var filterAttribute = new FilterAttribute(
                id,
                new AnyIdentifierCollectionFilterAttributeValue(value),
                true);
            return filterAttribute;
        }
    }
}
