using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.PartyBar
{
    public interface IGameObjectToPartyBarPortraitConverter
    {
        IPartyBarPortraitViewModel Convert(IGameObject actor);
    }
}
