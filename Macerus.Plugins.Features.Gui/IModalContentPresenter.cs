using System.Collections.Generic;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui
{
    public interface IModalContentPresenter
    {
        Task PresentAsync(
            object content,
            IEnumerable<IModalButtonViewModel> buttons);
    }
}
