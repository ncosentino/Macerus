using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class SpawnWithEquipmentGeneratorComponent : IGeneratorComponent
    {
        public SpawnWithEquipmentGeneratorComponent(
            IIdentifier dropTableId,
            bool bestEffortEquip,
            IIdentifier failedEquipInventoryId)
        {
            DropTableId = dropTableId;
            BestEffortEquip = bestEffortEquip;
            FailedEquipInventoryId = failedEquipInventoryId;
        }

        public IIdentifier DropTableId { get; }

        public bool BestEffortEquip { get; }

        public IIdentifier FailedEquipInventoryId { get; }
    }
}
