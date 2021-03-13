using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;

namespace Macerus.Shared.Behaviors.Filtering
{
    public sealed class FilterContextAmenity : IFilterContextAmenity
    {
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;

        public FilterContextAmenity(
            IFilterContextFactory filterContextFactory,
            IGameObjectIdentifiers gameObjectIdentifiers)
        {
            _filterContextFactory = filterContextFactory;
            _gameObjectIdentifiers = gameObjectIdentifiers;
        }

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
            var context = _filterContextFactory
                .CreateContext(
                filterContext,
                new FilterAttribute(
                    _gameObjectIdentifiers.FilterContextTemplateId,
                    new IdentifierFilterAttributeValue(templateId),
                    true));
            return context;
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
    }
}
