using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class DutyCycle : IDutyCycle
    {
        public DutyCycle(
            Channel channel,
            IWaveInstruction cycle)
        {
            Channel = channel;
            Cycle = cycle;
        }
        
        public Channel Channel { get; }

        public IWaveInstruction Cycle { get; }
    }
}
