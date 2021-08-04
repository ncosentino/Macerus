using System.Collections.Generic;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class NoneModalContentPresenter : IModalContentPresenter
    {
        public Task PresentAsync(
            object content, 
            IEnumerable<IModalButtonViewModel> buttons) => 
            Task.CompletedTask;
    }
}
