using System;
using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public interface IRpcClient
    {
        #region Methods
        TResponse Send<TRequest, TResponse>(TRequest request, TimeSpan timeout)
            where TRequest : IRequest
            where TResponse : IResponse;

        TResponse Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse;

        void Send<TRequest, TResponse>(
            TRequest request, 
            TimeSpan timeout,
            Action<TResponse> callback)
            where TRequest : IRequest
            where TResponse : IResponse;

        void Send<TRequest, TResponse>(
            TRequest request,
            Action<TResponse> callback)
            where TRequest : IRequest
            where TResponse : IResponse;
        #endregion
    }
}