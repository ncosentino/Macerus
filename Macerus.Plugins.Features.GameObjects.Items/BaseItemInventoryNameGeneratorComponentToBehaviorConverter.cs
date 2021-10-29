using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Plugins.Features.GameObjects.Items.Generation;
using Macerus.Plugins.Features.Resources;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class BaseItemInventoryNameGeneratorComponentToBehaviorConverter
        : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IStringResourceProvider _stringResourceProvider;

        public BaseItemInventoryNameGeneratorComponentToBehaviorConverter(IStringResourceProvider stringResourceProvider)
        {
            _stringResourceProvider = stringResourceProvider;
        }

        public Type ComponentType { get; } = typeof(BaseItemInventoryNameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var nameGeneratorComponent = (BaseItemInventoryNameGeneratorComponent)generatorComponent;
            var displayName = _stringResourceProvider.GetString(
                nameGeneratorComponent.StringResourceId,
                CultureInfo.InvariantCulture);
            yield return new BaseItemInventoryDisplayName(
                nameGeneratorComponent.StringResourceId,
                displayName);
        }
    }
}
