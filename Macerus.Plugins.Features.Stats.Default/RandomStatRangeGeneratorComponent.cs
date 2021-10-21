
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Stats.Default
{
    public sealed class RandomStatRangeGeneratorComponent : IGeneratorComponent
    {
        public RandomStatRangeGeneratorComponent(
            IIdentifier statDefinitionId, 
            double minimum,
            double maximum, 
            int decimals)
        {
            StatDefinitionId = statDefinitionId;
            Minimum = minimum;
            Maximum = maximum;
            Decimals = decimals;
        }

        public IIdentifier StatDefinitionId { get; }

        public double Minimum { get; }

        public double Maximum { get; }

        public int Decimals { get; }
    }
}
