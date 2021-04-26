using ProjectXyz.Api.Systems;
using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui.Api
{
    public interface IUserInterfaceSystem : IDiscoverableSystem
    {
        void RegisterUpdater(double interval, Func<Task> updateTaskFactory);
    }
}
