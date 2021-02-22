using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class RandomRangeExpressionFilterComponent : IFilterComponent
    {
        public RandomRangeExpressionFilterComponent(
            IIdentifier statDefinitionId,
            string @operator,
            ICalculationPriority priority,
            double minimumInclusive,
            double maximumInclusive)
        {
            StatDefinitionId = statDefinitionId;
            Operator = @operator;
            Priority = priority;
            MinimumInclusive = minimumInclusive;
            MaximumInclusive = maximumInclusive;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();
        
        public IIdentifier StatDefinitionId { get; }
        
        public string Operator { get; }
        
        public ICalculationPriority Priority { get; }
        
        public double MinimumInclusive { get; }
        
        public double MaximumInclusive { get; }
    }
}
