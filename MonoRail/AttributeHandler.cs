using MonoRail.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MonoRail
{
    internal sealed class AttributeHandler
    {
        private AttributeHandler() { }

        public static AttributeHandler Current { get; } = new AttributeHandler();

        private HashSet<Type> registeredTypes = new HashSet<Type>();

        public void RegisterAttributesForType(Type type)
        {
            if (!registeredTypes.Contains(type))
            {
                if (typeof(GameObject).IsAssignableFrom(type))
                {
                    foreach (var attribute in type.GetCustomAttributes<BehaviourAttribute>())
                    {
                        attribute.RegisterBehaviour(type);
                    }
                }
                registeredTypes.Add(type);
            }
        }
    }
}
