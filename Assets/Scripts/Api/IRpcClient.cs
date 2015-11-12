using System;
using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public interface IRpcClient
    {
        TResponse Send<TRequest, TResponse>(TRequest request, TimeSpan timeout)
            where TRequest : IRequest
            where TResponse : IResponse;
    }
}