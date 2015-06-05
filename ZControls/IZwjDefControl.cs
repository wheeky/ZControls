using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEMS.Controls
{
    public interface IZwjDefControl
    {
        event EventHandler OnValueChange;

        string Name
        { get; set; }

        bool Enabled
        { get; set; }

    }
}
