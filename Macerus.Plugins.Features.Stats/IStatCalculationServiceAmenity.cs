using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Stats
{
    public interface IStatCalculationServiceAmenity
    {
        double GetStatValue(IGameObject gameObject, IIdentifier statDefinitionId);
        double GetStatValue(IGameObject gameObject, IIdentifier statDefinitionId, IStatCalculationContext statCalculationContext);
        IReadOnlyDictionary<IIdentifier, double> GetStatValues(IGameObject gameObject, IEnumerable<IIdentifier> statDefinitionIds);
    }
}