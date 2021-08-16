namespace Macerus.Plugins.Features.Summoning
{
    public interface ISummonHandlerLoadOrder
    {
        int GetOrder(IDiscoverableSummonHandler handler);
    }
}
