using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Specifies methods for configuring and loading GameObjects into a level from a resource.
    /// </summary>
    public interface ILevelLoader
    {
        void LoadLevel(Level level);
    }
}
