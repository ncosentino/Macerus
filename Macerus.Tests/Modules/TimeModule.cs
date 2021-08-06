using System;

using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.ElapsedTime;

namespace Macerus.Tests
{
    public sealed class TimeModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestTimeManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class TestTimeManager : IRealTimeManager
        {
            private DateTime _dateTime;

            public DateTime GetTimeUtc() => _dateTime;

            public void SetTimeUtc(DateTime dateTime) => _dateTime = dateTime;
        }
    }

    public interface IRealTimeManager : IRealTimeProvider
    {
        void SetTimeUtc(DateTime dateTime);
    }
}
