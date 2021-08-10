using System;
using Autofac;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;

using Xunit.Sdk;

namespace Macerus.Tests
{
    public sealed class ConsoleLoggerModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ConsoleLogger>()
                .As<ILogger>()
                .SingleInstance();
        }

        private sealed class ConsoleLogger : ILogger
        {
            public void Debug(string message) => Debug(message, null);

            public void Debug(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"DEBUG: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }

            public void Error(string message) => Error(message, null);

            public void Error(string message, object data)
            {
                throw new XunitException(
                    "An error was encountered:\r\n" +
                    $"{message}\r\n" +
                    $"Exception:\r\n" +
                    $"{data}");
            }

            public void Info(string message)
            {
                throw new NotImplementedException();
            }

            public void Info(string message, object data)
            {
                throw new NotImplementedException();
            }

            public void Warn(string message)
            {
                throw new NotImplementedException();
            }

            public void Warn(string message, object data)
            {
                throw new NotImplementedException();
            }
        }
    }
}
