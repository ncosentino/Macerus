using System;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine
{
    public sealed class WaveEngineSpecifications : IEngineSpecifications
    {
        public Tuple<int, int> PitchRange { get; } = new Tuple<int, int>(0, 0xFF);

        public Tuple<int, int> LengthRange { get; } = new Tuple<int, int>(0, 0x7F);

        public Tuple<int, int> VolumeRange { get; } = new Tuple<int, int>(50, 50);
    }
}
