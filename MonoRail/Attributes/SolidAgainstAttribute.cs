using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Attributes
{
    /// <summary>
    /// Specifies that a type of GameObject cannot pass through another type of GameObject.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SolidAgainstAttribute : BehaviourAttribute
    {
        /// <summary>
        /// The other type of GameObject that the GameObject this attribute is applied on will not be able to pass through.
        /// </summary>
        public Type Other { get; }
        
        /// <summary>
        /// The sides of the other type of GameObject which cannot be passed through from.
        /// </summary>
        public Side SolidSides { get; }

        /// <summary>
        /// Indicates whether to use pixel collision checking.
        /// </summary>
        public bool CheckPixels { get; }
        
        /// <summary>
        /// Initializes a new instance of the SolidAgainstAttribute with the other GameObject to be solid against, the solid sides of the other object and the checkPixels value.
        /// </summary>
        /// <param name="other">The other type of GameObject that this type of GameObject cannot pass through.</param>
        /// <param name="solidSides">The sides of the other GameObject that this GameObject cannot pass though from.</param>
        /// <param name="checkPixels">Indicates whether to use pixel collision checking.</param>
        public SolidAgainstAttribute(Type other, Side solidSides, bool checkPixels)
        {
            Other = other;
            SolidSides = solidSides;
            CheckPixels = checkPixels;
        }

        public override void RegisterBehaviour(Type onType)
        {
            typeof(SolidHandler).GetMethod(nameof(SolidHandler.Current.RegisterSolidState))
                .MakeGenericMethod(onType, Other)
                .Invoke(SolidHandler.Current, new object[] { SolidSides, CheckPixels });
        }
    }
}
