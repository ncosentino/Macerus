using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine
{
    public sealed class WaveEngineSpecifications : IEngineSpecifications
    {
        public Tuple<int, int> PitchRange { get; } = new Tuple<int, int>(0, 0xFF);

        public Tuple<int, int> LengthRange { get; } = new Tuple<int, int>(0, 0x7F);

        public Tuple<int, int> VolumeRange { get; } = new Tuple<int, int>(50, 50);
    }
}
