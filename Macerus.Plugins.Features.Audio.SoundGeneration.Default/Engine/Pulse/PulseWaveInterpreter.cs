using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Pulse
{
    public sealed class PulseWaveInterpreter : IWaveChannelInterpreter
    {
        public IChannelSpecification ChannelSpecification { get; } = new PulseChannelSpecification();

        public long FindWaveLength(IWaveChannelDefinition waveDefinition, int length, IReadOnlyCollection<double> wave)
        {
            var leftovers = 0;
            var waveLength = 0L;
            foreach (var instruction in waveDefinition.Instructions)
            {
                if (instruction.Ops.Length == 4)
                {
                    var notes = instruction.Ops;

                    var subframes = ((length + 0x100) * (notes[0] + 1)) + leftovers;
                    var noteLength = SamplingSettings.SamplesPerFrame * (subframes >> 8);
                    leftovers = subframes & 0xFF;
                    waveLength += noteLength;
                }
            }

            return waveLength + leftovers;
        }

        public IReadOnlyCollection<double> Interpret(IWaveChannelDefinition definition, int pitch, int length, int cutoff)
        {
            var wave = new List<double>();
            var perc = 0d;
            var duty = 0;
            var commandIndex = 0;
            var sampleIndex = 0;
            var leftovers = 0;

            var pulseLength = definition.Instructions.Count;

            Func<int, double, bool> calculateDuty = (int dutyCycle, double p) =>
            {
                switch (dutyCycle)
                {
                    case 0: return (p >= 4 / 8d && p < 5 / 8d);
                    case 1: return (p >= 4 / 8d && p < 6 / 8d);
                    case 2: return (p >= 2 / 8d && p < 6 / 8d);
                    case 3: return (p < 4 / 8d || p >= 6 / 8d);
                }
                return false;
            };

            Func<int, int, double> getSample = (int bin, int volume) =>
            {
                return ((2 * bin) - 1) * (-volume / 16d);
            };

            foreach (var command in definition.Instructions)
            {
                if (command.Ops.Length == 1)
                {
                    duty = command.Ops[0];
                }

                if (command.Ops.Length == 4)
                {
                    var notes = command.Ops;

                    var subframes = ((length + 0x100) * (notes[0] + 1)) + leftovers;
                    var sampleCount = SamplingSettings.SamplesPerFrame * (subframes >> 8);

                    leftovers = subframes & 0xFF;

                    var volume = notes[1];
                    var volumeFade = notes[2];
                    long intermediate = SamplingSettings.SampleRate * ((long)(2048 - ((notes[3] + pitch) & 0x7FF)));
                    long period = intermediate / 131072L;

                    for (var i = 0; i < 2500000 && (i < sampleCount || (commandIndex == pulseLength - 1 && volume > 0)); i++)
                    {
                        var sample = getSample(
                            calculateDuty(duty & 3, perc) ? 1 : 0,
                            volume);

                        wave.Add(sample);

                        perc += 1 / (double)period;
                        perc = perc >= 1 ? perc - 1 : perc;
                        sampleIndex++;

                        // once per frame, adjust duty
                        if (i < sampleCount && sampleIndex % (SamplingSettings.SamplesPerFrame) == 0)
                        {
                            duty = ((duty & 0x3F) << 2) | ((duty & 0xC0) >> 6);
                        }

                        // once per frame * fadeamount, adjust volume
                        if (volumeFade != 0 && (i + 1) % (SamplingSettings.SamplesPerFrame * Math.Abs(volumeFade)) == 0)
                        {
                            volume += (volumeFade < 0 ? 1 : -1);
                            volume = volume < 0 ? 0 : (volume > 0x0F ? 0x0F : volume);
                        }
                    }
                }

                commandIndex++;
            }

            return wave.ToArray();
        }
    }
}
