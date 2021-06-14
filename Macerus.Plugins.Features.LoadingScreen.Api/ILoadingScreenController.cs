using System;

namespace Macerus.Plugins.Features.LoadingScreen.Api
{
    public interface ILoadingScreenController
    {
        void BeginLoad(
            Action startWorkCallback,
            Func<double> checkWorkProgressCallback,
            Action doWhenDoneCallback);
    }
}
