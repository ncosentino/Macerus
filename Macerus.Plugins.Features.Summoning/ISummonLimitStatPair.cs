
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonLimitStatPair
    {
        IIdentifier Id { get; }

        IIdentifier CurrentStatDefinitionId { get; }

        IIdentifier MaximumStatDefinitionId { get; }
    }
}
