using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gui
{
    public interface IDoubleClickBehaviour
    {
        #region Events
        event EventHandler<EventArgs> DoubleClick;
        #endregion
    }
}
