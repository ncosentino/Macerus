using System;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api
{
    public interface IEngineSpecifications
    {
        Tuple<int, int> PitchRange { get; }

        Tuple<int, int> LengthRange { get; }

        Tuple<int, int> VolumeRange { get; }

    }
}
