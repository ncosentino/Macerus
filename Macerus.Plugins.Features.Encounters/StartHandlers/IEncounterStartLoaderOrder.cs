namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterStartLoaderOrder
    {
        int GetOrder(IStartEncounterHandler handler);
    }
}
