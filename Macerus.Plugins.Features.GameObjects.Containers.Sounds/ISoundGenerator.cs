using ProjectXyz.Plugins.Features.Filtering.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds
{
    public interface ISoundGenerator
    {
        float[] GenerateSound(IFilterContext filterContext);
    }
}
