using System;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Noise;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Pulse;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default
{
    public sealed class SoundGenerator : ISoundGenerator
    {
        private readonly IWaveEngine _waveEngine;
        private readonly SoundPatternPipeline _pipeline;
        private readonly ISoundPatternRepository _soundPatternRepository;

        public SoundGenerator()
        {
            _waveEngine = new WaveEngine(
                new PulseWaveInterpreter(),
                new PulseWaveInterpreter(),
                new NoiseWaveInterpreter());

            _pipeline = new SoundPatternPipeline();

            _soundPatternRepository = new SoundPatternRepository();
        }

        public float[] GenerateSound(IFilterContext filterContext)
        {
            var channelDefinitions = _waveEngine
                .ChannelSpecifications
                .Select(x => new
                {
                    Spec = x,
                    Pattern = _pipeline.GetPatternForChannel(x.Channel)
                })
                .Select(x => new
                {
                    x.Spec,
                    Pattern = Tuple.Create(x.Pattern.Item1, x.Pattern.Item2, _pipeline.TransformPattern(x.Pattern.Item3))
                })
                .Select(x => _pipeline.MaterializePatternToChannelDefinition(
                    x.Pattern.Item1,
                    x.Pattern.Item2,
                    x.Pattern.Item3,
                    x.Spec));

            var random = new Random();
            var length = _soundPatternRepository.Lengths.Random(random);

            // The engine also publishes pitch, volume and length 
            // specs that we can use here.
            var wave = _waveEngine.ConvertDefinitionToWave(
                new WaveDefinition(channelDefinitions),
                0x0,
                length,
                50);

            return wave
                .Select(x => (float)x)
                .ToArray();
        }
    }
}
