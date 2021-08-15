namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningBehaviorFactory : ISummoningBehaviorFactory
    {
        private readonly ISummonHandlerFacade _summonHandlerFacade;

        public SummoningBehaviorFactory(ISummonHandlerFacade summonHandlerFacade)
        {
            _summonHandlerFacade = summonHandlerFacade;
        }

        public ISummoningBehavior Create()
        {
            var behavior = new SummoningBehavior(_summonHandlerFacade);
            return behavior;
        }
    }
}
