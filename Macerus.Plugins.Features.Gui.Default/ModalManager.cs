using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui.Default
{

    public sealed class ModalManager : IModalManager
    {
        private readonly IModalContentPresenter _modalContentPresenter;
        private readonly IModalButtonViewModelFactory _modalButtonViewModelFactory;

        public ModalManager(
            IModalContentPresenter modalContentPresenter,
            IModalButtonViewModelFactory modalButtonViewModelFactory)
        {
            _modalContentPresenter = modalContentPresenter;
            _modalButtonViewModelFactory = modalButtonViewModelFactory;
        }

        public async Task ShowAndWaitMessageBoxAsync(string message)
        {
            IModalButtonViewModel resultButton = null;
            var okButton = _modalButtonViewModelFactory.CreateOkButton(x => resultButton = x);
            await ShowAndWaitDialogAsync(
                message,
                async () =>
                {
                    while (resultButton == null)
                    {
                        await Task.Yield();
                    }
                },
                okButton)
                .ConfigureAwait(false);
        }

        public Task ShowMessageBoxAsync(string message) =>
            ShowDialogAsync(message, new[] { _modalButtonViewModelFactory.CreateOkButton(_ => { }) });

        public async Task<bool> ShowAndWaitYesNoAsync(string message)
        {
            IModalButtonViewModel resultButton = null;
            var yesButton = _modalButtonViewModelFactory.CreateYesButton(x => resultButton = x);
            var noButton = _modalButtonViewModelFactory.CreateNoButton(x => resultButton = x);
            await ShowAndWaitDialogAsync(
                message,
                async () =>
                {
                    while (resultButton == null)
                    {
                        await Task.Yield();
                    }
                },
                yesButton,
                noButton)
                .ConfigureAwait(false);
            var result = resultButton == yesButton;
            return result;
        }

        public async Task ShowYesNoAsync(
            string message,
            Action<bool> callback)
        {
            var yesButton = _modalButtonViewModelFactory.CreateYesButton(_ => callback.Invoke(true));
            var noButton = _modalButtonViewModelFactory.CreateNoButton(_ => callback.Invoke(false));
            await ShowDialogAsync(
                message,
                new[]
                {
                    yesButton,
                    noButton
                })
                .ConfigureAwait(false);
        }

        public async Task<bool?> ShowAndWaitYesNoCancelAsync(string message)
        {
            IModalButtonViewModel resultButton = null;
            var yesButton = _modalButtonViewModelFactory.CreateYesButton(x => resultButton = x);
            var noButton = _modalButtonViewModelFactory.CreateNoButton(x => resultButton = x);
            var cancelButton = _modalButtonViewModelFactory.CreateCancelButton(x => resultButton = x);
            await ShowAndWaitDialogAsync(
                message,
                async () =>
                {
                    while (resultButton == null)
                    {
                        await Task.Yield();
                    }
                },
                yesButton,
                noButton,
                cancelButton)
                .ConfigureAwait(false);
            if (resultButton == yesButton)
            {
                return true;
            }

            if (resultButton == noButton)
            {
                return false;
            }

            return null;
        }

        public async Task ShowYesNoCancelAsync(
            string message,
            Action<bool?> callback)
        {
            var yesButton = _modalButtonViewModelFactory.CreateYesButton(_ => callback.Invoke(true));
            var noButton = _modalButtonViewModelFactory.CreateNoButton(_ => callback.Invoke(false));
            var cancelButton = _modalButtonViewModelFactory.CreateNoButton(_ => callback.Invoke(null));
            await ShowDialogAsync(
                message,
                new[]
                {
                    yesButton,
                    noButton,
                    cancelButton,
                })
                .ConfigureAwait(false);
        }

        public async Task ShowAndWaitDialogAsync(
            object content,
            Func<Task> waitCallback,
            IModalButtonViewModel button,
            params IModalButtonViewModel[] otherButtons)
        {
            await ShowAndWaitDialogAsync(
                content,
                waitCallback,
                new[] { button }.Concat(otherButtons))
                .ConfigureAwait(false);
        }

        public async Task ShowAndWaitDialogAsync(
            object content,
            Func<Task> waitCallback,
            IEnumerable<IModalButtonViewModel> buttons)
        {
            await ShowDialogAsync(
                content,
                buttons)
                .ConfigureAwait(false);
            await waitCallback
                .Invoke()
                .ConfigureAwait(false);
        }

        public async Task ShowDialogAsync(
            object content,
            IEnumerable<IModalButtonViewModel> buttons)
        {
            await _modalContentPresenter
                .PresentAsync(
                    content,
                    buttons)
                .ConfigureAwait(false);
        }
    }
}
