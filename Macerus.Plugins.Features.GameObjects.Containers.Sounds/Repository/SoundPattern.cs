using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class SoundPattern : ISoundPattern
    {
        public SoundPattern(
            Channel channel,
            IEnumerable<IWaveInstruction> transforms)
        {
            Channel = channel;
            Transforms = transforms.ToArray();

        }

        public Channel Channel { get; }
        
        public IReadOnlyCollection<IWaveInstruction> Transforms { get; }
    }
}
