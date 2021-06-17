using System;

using Macerus.Game.Api;

namespace Macerus.Game
{
    public sealed class NoneApplication : IApplication
    {
        public void Exit() => Environment.Exit(0);
    }
}
