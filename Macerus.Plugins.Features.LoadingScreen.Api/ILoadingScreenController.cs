using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.LoadingScreen.Api
{
    public interface ILoadingScreenController
    {
        void BeginLoad(
            Func<Task> startWorkCallbackAsync,
            Func<Task<double>> checkWorkProgressCallbackAsync,
            Func<Task> doWhenDoneCallbackAsync);
    }
}
