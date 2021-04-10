using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Combat
{
    public sealed class CombatStatIdentifiers : ICombatStatIdentifiers
    {
        public IIdentifier CurrentLifeStatId => new IntIdentifier(2);
    }
}
