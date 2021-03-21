using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api
{
    public interface IStartingNote
    {
        Channel Channel { get; }

        IWaveInstruction Note { get; }
    }
}
