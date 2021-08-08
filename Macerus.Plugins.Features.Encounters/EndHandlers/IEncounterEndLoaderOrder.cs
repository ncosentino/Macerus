namespace Macerus.Plugins.Features.Encounters.EndHandlers
{
    public interface IEncounterEndLoaderOrder
    {
        int GetOrder(IEndEncounterHandler handler);
    }
}
