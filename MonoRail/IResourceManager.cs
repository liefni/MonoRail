using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Loads game resources.
    /// </summary>
    public interface IResourceManager
    {
        TInterface GetSprite<TInterface>(string resourceName)
            where TInterface : ISprite;
        IView GetView();    
    }
}
