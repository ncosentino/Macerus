using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui
{
    public interface IModalManager
    {
        Task ShowAndWaitDialogAsync(
            object content,
            Func<Task> waitCallback,
            IEnumerable<IModalButtonViewModel> buttons);

        Task ShowAndWaitDialogAsync(
            object content,
            Func<Task> waitCallback,
            IModalButtonViewModel button,
            params IModalButtonViewModel[] otherButtons);

        Task ShowAndWaitMessageBoxAsync(string message);

        Task<bool> ShowAndWaitYesNoAsync(string message);

        Task<bool?> ShowAndWaitYesNoCancelAsync(string message);

        Task ShowDialogAsync(
            object content,
            IEnumerable<IModalButtonViewModel> buttons);

        Task ShowMessageBoxAsync(string message);

        Task ShowYesNoAsync(
            string message,
            Action<bool> callback);

        Task ShowYesNoCancelAsync(
            string message,
            Action<bool?> callback);
    }
}
