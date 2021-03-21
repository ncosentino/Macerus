using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine
{
    public sealed class WaveEngine : IWaveEngine
    {
        private readonly IReadOnlyCollection<IWaveChannelInterpreter> _interpreters;

        public WaveEngine(params IWaveChannelInterpreter[] interpreters)
        {
            _interpreters = interpreters.ToArray();

            ChannelSpecifications = _interpreters.Select(x => x.ChannelSpecification).ToArray();
        }

        public IReadOnlyCollection<IChannelSpecification> ChannelSpecifications { get; }

        public IEngineSpecifications EngineSpecifications { get; } = new WaveEngineSpecifications();

        public double[] ConvertDefinitionToWave(IWaveDefinition definition, int pitch, int length, int volume)
        {
            length = length - 0x80;

            var channelWaves = new List<Tuple<long, IReadOnlyCollection<double>>>();
            foreach(var channel in definition.Channels)
            {
                var interpreter = _interpreters.FirstOrDefault(x => x.ChannelSpecification.Channel == channel.Type);

                if (interpreter == null)
                {
                    channelWaves.Add(Tuple.Create(0L, (IReadOnlyCollection<double>)new double[0]));
                }

                var wave = interpreter.Interpret(
                    channel,
                    pitch,
                    length, 
                    channelWaves.Count == 0 ? 0 : (int)channelWaves.Max(x => x.Item1) - SamplingSettings.SamplesPerFrame);
                var waveLength = interpreter.FindWaveLength(channel, length, wave);

                channelWaves.Add(Tuple.Create(waveLength, wave));
            }

            var totalLength = channelWaves.Max(x => x.Item2.Count);

            var combinedWave = new double[totalLength];
            Array.Clear(combinedWave, 0, totalLength);

            foreach (var channelWave in channelWaves)
            {
                var cw = channelWave.Item2.ToArray();
                for (var i = 0; i < cw.Length; i++)
                {
                    combinedWave[i] += cw[i] / channelWaves.Count;
                }
            }

            var scaledDown = new List<double>();
            var convRatio = SamplingSettings.SampleRate / 48000d;
            for (var i = 0; i * convRatio < combinedWave.Length - 1; i++)
            {
                var pt = (int)Math.Floor(i * convRatio);
                var frac = i * convRatio - pt;
                var converted = (volume / 256d) * ((1 - frac) * combinedWave[pt] + frac * combinedWave[pt + 1]);
                scaledDown.Add(converted);
            }

            return scaledDown.ToArray();
        }
    }
}
