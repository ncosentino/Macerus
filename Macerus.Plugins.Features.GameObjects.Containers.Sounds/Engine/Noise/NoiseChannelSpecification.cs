using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Noise
{
    public sealed class NoiseChannelSpecification : IChannelSpecification
    {
        public Channel Channel => Channel.Noise;

        public IReadOnlyCollection<IOpSpecification> OpSpecifications => new IOpSpecification[]
        {
            new OpSpecification(
                "Length",
                "Length of the note in frames",
                0x01,
                0x0F),
            new OpSpecification(
                "Initial volume",
                "The starting volume of the note",
                0x00,
                0x0F),
            new OpSpecification(
                "Volume sweep",
                "Quieter over time if positive. Louder if negative. Higher magnitude changes volume more quickly",
                0x00,
                0x07),
            new OpSpecification(
                "Noise sample",
                "Used as the basis for sampling the noise channel",
                0x00,
                0xAC),
        };
    }
}
