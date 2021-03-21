using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Pulse
{
    public sealed class PulseChannelSpecification : IChannelSpecification
    {
        public Channel Channel => Channel.Pulse;

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
                "Frequency",
                "The frequency of the note to play, in hertz",
                0x00,
                0x7E2),
        };
    }
}
