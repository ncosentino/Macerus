using System;

namespace Macerus.Plugins.Features.LoadingScreen.Api
{
    public interface ILoadingScreenController
    {
        void Load(Action doWorkCallback, Action doWhenDoneCallback);
    }
}
