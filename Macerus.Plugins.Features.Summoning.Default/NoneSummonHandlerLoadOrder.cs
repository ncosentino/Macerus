namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class NoneSummonHandlerLoadOrder : ISummonHandlerLoadOrder
    {
        public int GetOrder(IDiscoverableSummonHandler handler) => handler.Priority.HasValue
            ? handler.Priority.Value
            : int.MaxValue;
    }
}
