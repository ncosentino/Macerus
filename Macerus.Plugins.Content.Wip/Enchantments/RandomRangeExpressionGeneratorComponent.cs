using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Framework;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class RandomRangeExpressionGeneratorComponent : IGeneratorComponent
    {
        public RandomRangeExpressionGeneratorComponent(
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

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();
        
        public IIdentifier StatDefinitionId { get; }
        
        public string Operator { get; }
        
        public ICalculationPriority Priority { get; }
        
        public double MinimumInclusive { get; }
        
        public double MaximumInclusive { get; }
    }
}
