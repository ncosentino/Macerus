namespace Macerus.Plugins.Features.Summoning
{
    public interface IDiscoverableSummonHandler : ISummonHandler
    {
        int? Priority { get; }
    }
}
