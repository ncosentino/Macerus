using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class StartingNote : IStartingNote
    {
        public StartingNote(
            Channel channel,
            IWaveInstruction note)
        {
            Channel = channel;
            Note = note;

        }

        public Channel Channel { get; }

        public IWaveInstruction Note { get; }
    }
}
