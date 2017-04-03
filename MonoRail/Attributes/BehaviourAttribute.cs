using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Attributes
{
    /// <summary>
    /// Attribute for specifying a behaviour on a type of GameObject
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BehaviourAttribute : Attribute
    {
        /// <summary>
        /// When overridden, sets up the behviour for the type of object it is used on.
        /// </summary>
        /// <param name="onType">The type which the attribute is used on.</param>
        public abstract void RegisterBehaviour(Type onType);
    }
}
