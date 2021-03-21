using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Pulse
{
    // TODO: This isn't consumed anywhere yet, because these commands aren't as dynamic as the others. 
    // Will need to add handling for multiple specs per channel eventually.
    public sealed class DutyCycleSpecification : IChannelSpecification
    {
        public Channel Channel => Channel.Pulse;

        public IReadOnlyCollection<IOpSpecification> OpSpecifications => new IOpSpecification[]
        {
            new OpSpecification(
                "Duty cycle",
                "The duty cycle to use until the next duty cycle command",
                0x00,
                0xFA),
        };
    }
}
