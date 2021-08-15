using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonHandler
    {
        Task<ISummoningContext> HandleSummoningAsync(ISummoningContext summoningContext);
    }
}
