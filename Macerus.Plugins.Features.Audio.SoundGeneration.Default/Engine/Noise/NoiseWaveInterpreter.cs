using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Noise
{
    public sealed class NoiseWaveInterpreter : IWaveChannelInterpreter
    {
        public IChannelSpecification ChannelSpecification { get; } = new NoiseChannelSpecification();

        public long FindWaveLength(IWaveChannelDefinition waveDefinition, int length, IReadOnlyCollection<double> wave)
        {
            return wave.Count;
        }

        public IReadOnlyCollection<double> Interpret(IWaveChannelDefinition definition, int pitch, int length, int cutoff)
        {
            var wave = new List<double>();

            var sampleIndex = 0;
            var leftovers = 0;
            var commandIndex = 0;
            var noiseBuffer = 0x7FFF;

            Func<int, int, double> getSample = (int bin, int volume) =>
            {
                return ((2 * bin) - 1) * (-volume / 16d);
            };

            foreach (var command in definition.Instructions)
            {
                var notes = command.Ops;

                var subframes = ((length + 0x100) * (notes[0] + 1)) + leftovers;
                var sampleCount = SamplingSettings.SamplesPerFrame * (subframes >> 8);

                leftovers = subframes & 0xFF;

                // volume and fade control
                var volume = notes[1];
                var volumeFade = notes[2];
                var parameters = (notes[3] + (sampleIndex >= cutoff ? 0 : pitch)) & 0xFF;

                // apply this note
                var shift = (parameters >> 4) & 0xF;
                shift = shift > 0xD ? shift & 0xD : shift; // not sure how to deal with E or F, but its so low you can hardly notice it anyway
                var divider = parameters & 0x7;
                var width = (parameters & 0x8) == 0x8;
                noiseBuffer = 0x7FFF;

                for (var i = 0; i < 2500000 && (i < sampleCount || (commandIndex == definition.Instructions.Count - 1 && volume > 0)); i++)
                {
                    var bit0 = noiseBuffer & 1;
                    wave.Add(getSample(1 ^ bit0, volume));
                    sampleIndex++;

                    // according to params, update buffer
                    if (sampleIndex % (2 * (divider == 0 ? 0.5 : divider) * (1 << (shift + 1))) == 0)
                    {
                        var bit1 = (noiseBuffer >> 1) & 1;
                        noiseBuffer = (noiseBuffer >> 1) | ((bit0 ^ bit1) << 14);
                        if (width) noiseBuffer = (noiseBuffer >> 1) | ((bit0 ^ bit1) << 6);
                    }

                    // once per frame * fadeamount, adjust volume
                    if (volumeFade != 0 && (i + 1) % (SamplingSettings.SamplesPerFrame * Math.Abs(volumeFade)) == 0)
                    {
                        volume += (volumeFade < 0 ? 1 : -1);
                        volume = volume < 0 ? 0 : (volume > 0x0F ? 0x0F : volume);
                    }
                }

                commandIndex++;
            }

            return wave.ToArray();
        }
    }
}
