using ProjectXyz.Api.GameObjects.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Api.Behaviors
{
    public interface IMakeNoiseBehaviour : IBehavior
    {
        float[] GetNoise();
    }
}
