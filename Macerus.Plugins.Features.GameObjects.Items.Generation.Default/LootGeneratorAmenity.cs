using System.Collections.Generic;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Default
{
    public sealed class LootGeneratorAmenity : ILootGeneratorAmenity
    {
        private readonly IDropTableIdentifiers _dropTableIdentifiers;
        private readonly ILootGenerator _lootGenerator;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IDropTableRepositoryFacade _dropTableRepository;

        public LootGeneratorAmenity(
            IDropTableIdentifiers dropTableIdentifiers,
            ILootGenerator lootGenerator,
            IFilterContextAmenity filterContextAmenity,
            IDropTableRepositoryFacade dropTableRepository)
        {
            _dropTableIdentifiers = dropTableIdentifiers;
            _lootGenerator = lootGenerator;
            _filterContextAmenity = filterContextAmenity;
            _dropTableRepository = dropTableRepository;
        }

        public IEnumerable<IGameObject> GenerateLoot(
            IIdentifier dropTableId,
            IFilterContext lootGeneratorContext)
        {
            ArgumentContract.RequiresNotNull(dropTableId, nameof(dropTableId));
            ArgumentContract.RequiresNotNull(lootGeneratorContext, nameof(lootGeneratorContext));

            var dropTable = _dropTableRepository.GetForDropTableId(dropTableId);
            var dropTableAttribute = _filterContextAmenity.CreateRequiredAttribute(
                _dropTableIdentifiers.FilterContextDropTableIdentifier,
                dropTableId);

            var filterContext = lootGeneratorContext
                .WithAdditionalAttributes(new[] { dropTableAttribute })
                .WithRange(
                    dropTable.MinimumGenerateCount,
                    dropTable.MaximumGenerateCount);

            var generatedItems = _lootGenerator.GenerateLoot(filterContext);
            return generatedItems;
        }
    }
}
