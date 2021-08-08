using Macerus.Plugins.Features.Encounters.EndHandlers;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class NoneEncounterEndLoadOrder : IEncounterEndLoaderOrder
    {
        public int GetOrder(IEndEncounterHandler handler) => int.MaxValue;
    }
}
