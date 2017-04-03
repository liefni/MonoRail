using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Specifies constants defining sides of a rectangle.
    /// </summary>
    [Flags]
    public enum Side
    {
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        All = 15,
        None = 0,
    }
}
