using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Plugins.Features.Resources;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public sealed class UniqueItemInventoryNameGeneratorComponentToBehaviorConverter
        : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IStringResourceProvider _stringResourceProvider;

        public UniqueItemInventoryNameGeneratorComponentToBehaviorConverter(IStringResourceProvider stringResourceProvider)
        {
            _stringResourceProvider = stringResourceProvider;
        }

        public Type ComponentType { get; } = typeof(UniqueItemInventoryNameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var nameGeneratorComponent = (UniqueItemInventoryNameGeneratorComponent)generatorComponent;
            var displayName = _stringResourceProvider.GetString(
                nameGeneratorComponent.StringResourceId,
                CultureInfo.InvariantCulture);
            yield return new UniqueItemInventoryDisplayName(
                nameGeneratorComponent.StringResourceId,
                displayName);
        }
    }
}
