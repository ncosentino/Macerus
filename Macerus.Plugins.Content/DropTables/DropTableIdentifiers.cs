using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.DropTables
{
    public sealed class DropTableIdentifiers : IDropTableIdentifiers
    {
        public IIdentifier FilterContextDropTableIdentifier { get; } = new StringIdentifier("drop-table");
    }
}
