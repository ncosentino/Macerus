namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class NoneEncounterStartLoadOrder : IEncounterStartLoaderOrder
    {
        public int GetOrder(IStartEncounterHandler handler) => int.MaxValue;
    }
}
