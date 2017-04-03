using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    //Specifies methods for an object responsible for creating and configuring an object implementing ILevelLoader
    public interface ILevelFormat
    {
        /// <summary>
        /// Creates and configures an object that implement ILevelLoader
        /// </summary>
        /// <param name="level">Level to create ILevelLoader object for.</param>
        /// <returns>Object implementing ILevelLoader.</returns>
        ILevelLoader GetLevelLoader(Level level);
    }
}
