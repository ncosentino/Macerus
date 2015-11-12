using System;
using System.Collections.Generic;
using System.Reflection;
using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public interface IMessageDiscoverer
    {
        IDictionary<string, Type> Discover<TPayload>(IEnumerable<Assembly> assemblies)
            where TPayload : IPayload;
    }
}