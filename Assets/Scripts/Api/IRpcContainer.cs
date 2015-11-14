using System;

namespace Assets.Scripts.Api
{
    public interface IRpcContainer : IDisposable
    {
        IRpcClient Client { get; }
    }
}