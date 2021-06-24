using Macerus.Plugins.Features.Combat.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatAIFactory : ICombatAIFactory
    {
        private readonly PrimitiveAttackCombatAI.Factory _factory;

        public CombatAIFactory(PrimitiveAttackCombatAI.Factory factory)
        {
            _factory = factory;
        }

        public ICombatAI Create()
        {
            // FIXME: this should be able to provide different combat AI
            var combatAI = _factory.Invoke();
            return combatAI;
        }
    }
}
