
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonLimitStatPair : ISummonLimitStatPair
    {
        public SummonLimitStatPair(
            IIdentifier id,
            IIdentifier currentStatDefinitionId,
            IIdentifier maximumStatDefinitionId)
        {
            Id = id;
            CurrentStatDefinitionId = currentStatDefinitionId;
            MaximumStatDefinitionId = maximumStatDefinitionId;
        }

        public IIdentifier Id { get; }

        public IIdentifier CurrentStatDefinitionId { get; }

        public IIdentifier MaximumStatDefinitionId { get; }
    }
}
