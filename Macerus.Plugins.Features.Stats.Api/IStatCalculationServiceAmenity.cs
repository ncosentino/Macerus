using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Stats.Api
{
    public interface IStatCalculationServiceAmenity
    {
        double GetStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId);
        
        double GetStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId,
            IStatCalculationContext statCalculationContext);

        Task<double> GetStatValueAsync(
            IGameObject gameObject,
            IIdentifier statDefinitionId);

        Task<double> GetStatValueAsync(
            IGameObject gameObject,
            IIdentifier statDefinitionId,
            IStatCalculationContext statCalculationContext);

        IReadOnlyDictionary<IIdentifier, double> GetStatValues(
            IGameObject gameObject,
            IEnumerable<IIdentifier> statDefinitionIds);

        Task<IReadOnlyDictionary<IIdentifier, double>> GetStatValuesAsync(
            IGameObject gameObject,
            IEnumerable<IIdentifier> statDefinitionIds);
    }
}