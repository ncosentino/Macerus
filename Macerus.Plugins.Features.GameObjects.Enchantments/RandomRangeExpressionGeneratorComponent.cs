using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
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
        
        public IIdentifier StatDefinitionId { get; }
        
        public string Operator { get; }
        
        public ICalculationPriority Priority { get; }
        
        public double MinimumInclusive { get; }
        
        public double MaximumInclusive { get; }
    }
}
