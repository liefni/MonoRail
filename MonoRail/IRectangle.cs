using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Defines properties for objects that can be logically represented as a rectangle.
    /// </summary>
    public interface IRectangle
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
