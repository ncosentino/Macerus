using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api
{
    public interface IDutyCycle
    {
        Channel Channel { get; }

        IWaveInstruction Cycle { get; }
    }
}
