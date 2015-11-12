using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProjectXyz.Api.Messaging.Interface;

namespace Assets.Scripts.Api
{
    public sealed class MessageDiscoverer : IMessageDiscoverer
    {
        #region Constructors
        private MessageDiscoverer()
        {
        }
        #endregion

        #region Methods
        public static IMessageDiscoverer Create()
        {
            var discoverer = new MessageDiscoverer();
            return discoverer;
        }

        public IDictionary<string, Type> Discover<TPayload>(IEnumerable<Assembly> assemblies)
            where TPayload : IPayload
        {
            var targetType = typeof(TPayload);
            return assemblies
                .SelectMany(assembly => assembly
                    .GetTypes()
                    .Where(x => !x.IsInterface && !x.IsAbstract && targetType.IsAssignableFrom(x)))
                .ToDictionary(x => x.Name, x => x);
        }
        #endregion
    }
}