namespace Macerus.Plugins.Features.Encounters
{
    public interface IDiscoverableStartEncounterHandler : IStartEncounterHandler
    {
        int Priority { get; }
    }
}
