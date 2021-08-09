using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using Macerus.Plugins.Features.Gui;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Tests
{
    public sealed class GuiModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestModalContentPresenter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class TestModalContentPresenter : ITestModalContentPresenter
        {
            private Func<IReadOnlyCollection<IModalButtonViewModel>, Task> _callback;

            public async Task PresentAsync(
                object content,
                IEnumerable<IModalButtonViewModel> buttons)
            {
                if (_callback == null)
                {
                    throw new InvalidOperationException(
                        $"A callback must be provided during tests or else " +
                        $"modal dialog presentation would hang indefinitely " +
                        $"without user input being provided.");
                }

                try
                {
                    await _callback
                        .Invoke(buttons.ToArray())
                        .ConfigureAwait(false);
                }
                finally
                {
                    _callback = null;
                }
            }

            public void SetCallback(Func<IReadOnlyCollection<IModalButtonViewModel>, Task> callback)
            {
                _callback = callback;
            }
        }
    }

    public interface ITestModalContentPresenter : IModalContentPresenter
    {
        void SetCallback(Func<IReadOnlyCollection<IModalButtonViewModel>, Task> callback);
    }
}
