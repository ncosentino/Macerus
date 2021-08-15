using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonLimitStatPairRepository
    {
        Task<ISummonLimitStatPair> GetPairByIdAsync(IIdentifier id);
    }
}
