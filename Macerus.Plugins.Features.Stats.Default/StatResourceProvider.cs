using System;

using Macerus.Plugins.Features.Resources;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Stats.Default
{
    public sealed class StatResourceProvider : IStatResourceProvider
    {
        private readonly Lazy<IStringResourceRepository> _lazyStringResourceProvider;

        public StatResourceProvider(Lazy<IStringResourceRepository> lazyStringResourceProvider)
        {
            _lazyStringResourceProvider = lazyStringResourceProvider;
        }

        public string GetStatName(IIdentifier statDefinitionId)
        {
            // FIXME: actually do a mapping from stat definition ID to string
            // resource ID... information should be in the database, or if we
            // move away from a DB there should be a lookup file.
            return $"??STAT:{statDefinitionId}??";
        }
    }
}
