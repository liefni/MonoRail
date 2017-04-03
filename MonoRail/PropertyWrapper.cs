using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Contains methods for getting and setting a property on an object.
    /// </summary>
    public interface IPropertyWrapper
    {
        /// <summary>
        /// Gets the value of the wrapped property for the specified object.
        /// </summary>
        /// <param name="objectToWrap">The object to get the property value from.</param>
        /// <returns>The value of the wrapped property</returns>
        object Get(object objectToWrap);
        /// <summary>
        /// Sets the value of the wrapped property on the specified object.
        /// </summary>
        /// <param name="objectToWrap">The object to get the property value from.</param>
        /// <param name="value">The value to set the wrapped property to.</param>
        void Set(object objectToWrap, object value);
    }

    /// <summary>
    /// Contains PropertyWrappers for different properties.
    /// </summary>
    public static class PropertyWrapper
    {
        /// <summary>
        /// Gets a wrapper for the X property of an IRectangle
        /// </summary>
        public static XWrapper X { get; } = new XWrapper();
        /// <summary>
        /// Gets a wrapper for the Y property of an IRectangle
        /// </summary>
        public static YWrapper Y { get; } = new YWrapper();
        /// <summary>
        /// Gets a wrapper for the PreviousX property of a GameObject
        /// </summary>
        public static PreviousXWrapper PreviousX { get; } = new PreviousXWrapper();
        /// <summary>
        /// Gets a wrapper for the PreviousY property of a GameObject
        /// </summary>
        public static PreviousYWrapper PreviousY { get; } = new PreviousYWrapper();
        /// <summary>
        /// Gets a wrapper for the Width property of an IRectangle
        /// </summary>
        public static WidthWrapper Width { get; } = new WidthWrapper();
        /// <summary>
        /// Gets a wrapper for the Height property of an IRectangle
        /// </summary>
        public static HeightWrapper Height { get; } = new HeightWrapper();
        /// <summary>
        /// Gets a wrapper for the HSpeed property of a GameObject
        /// </summary>
        public static HSpeedWrapper HSpeed { get; } = new HSpeedWrapper();
        /// <summary>
        /// Gets a wrapper for the VSpeed property of a GameObject
        /// </summary>
        public static VSpeedWrapper Vspeed { get; } = new VSpeedWrapper();
    }

    /// <summary>
    /// Represents an object for wrapping properties on another object.
    /// </summary>
    /// <typeparam name="TValue">The value type of the property to wrap.</typeparam>
    /// <typeparam name="TObjectToWrap">The type of object the property is on.</typeparam>
    public abstract class PropertyWrapper<TValue, TObjectToWrap> : IPropertyWrapper
    {
        /// <summary>
        /// Gets the value of the wrapped property for the specified object.
        /// </summary>
        /// <param name="objectToWrap">The object to get the property value from.</param>
        /// <returns>The value of the wrapped property</returns>
        public abstract TValue Get(TObjectToWrap objectToWrap);
        /// <summary>
        /// Sets the value of the wrapped property on the specified object.
        /// </summary>
        /// <param name="objectToWrap">The object to set the property value on.</param>
        /// <param name="value">The value to set the wrapped property to.</param>
        public abstract void Set(TObjectToWrap objectToWrap, TValue value);

        /// <summary>
        /// Creates a new InstancePropertyWrapper that gets a sets value on a specific object.
        /// </summary>
        /// <param name="objectToWrap">The object to create the InstancePropertyWrapper for.</param>
        /// <returns>A new InstancePropertyWrapper for the specified object.</returns>
        public InstancePropertyWrapper<TValue, TObjectToWrap> ForObject(TObjectToWrap objectToWrap)
        {
            return new InstancePropertyWrapper<TValue, TObjectToWrap>(objectToWrap, this);
        }

        object IPropertyWrapper.Get(object objectToWrap)
        {
            return Get((TObjectToWrap)objectToWrap);
        }

        void IPropertyWrapper.Set(object objectToWrap, object value)
        {
            Set((TObjectToWrap)objectToWrap, (TValue)value);
        }
    }

    /// <summary>
    /// Specifies a property for getting or setting a wrapped property on a specific object.
    /// </summary>
    public class IInstancePropertyWrapper
    {
        /// <summary>
        /// Gets or sets the value of the wrapped property.
        /// </summary>
        object Value { get; set; }
    }

    /// <summary>
    /// Represents an object for getting and setting values of a wrapped property on a specific object.
    /// </summary>
    /// <typeparam name="TValue">The value type of the property.</typeparam>
    /// <typeparam name="TObjectToWrap">The type of object the property is on.</typeparam>
    public class InstancePropertyWrapper<TValue, TObjectToWrap> : IInstancePropertyWrapper
    {
        TObjectToWrap objectToWrap;
        PropertyWrapper<TValue, TObjectToWrap> propertyWrapper;
        /// <summary>
        /// Initilizes a new InstancePropertyWrapper for the specified object and property wrapper.
        /// </summary>
        /// <param name="objectToWrap">The object to get and set the wrapped property on.</param>
        /// <param name="propertyWrapper">The property wrapped specifying the wrapped property.</param>
        public InstancePropertyWrapper(TObjectToWrap objectToWrap, PropertyWrapper<TValue, TObjectToWrap> propertyWrapper)
        {
            this.objectToWrap = objectToWrap;
            this.propertyWrapper = propertyWrapper;
        }

        /// <summary>
        /// Gets or sets the value of the wrapped property.
        /// </summary>
        public TValue Value
        {
            get { return propertyWrapper.Get(objectToWrap); }
            set { propertyWrapper.Set(objectToWrap, value); }
        }
    }

    /// <summary>
    /// Represents a PropertyWrapper for either the X or Y property of an IRectangle.
    /// </summary>
    public abstract class PositionWrapper : PropertyWrapper<int, IRectangle> { }
    public class XWrapper : PositionWrapper
    {
        public override int Get(IRectangle objectToWrap) => objectToWrap.X;
        public override void Set(IRectangle objectToWrap, int value) => objectToWrap.X = value;
    }
    public class YWrapper : PositionWrapper
    {
        public override int Get(IRectangle objectToWrap) => objectToWrap.Y;
        public override void Set(IRectangle objectToWrap, int value) => objectToWrap.Y = value;
    }

    /// <summary>
    /// Represents a property wrapper for either the PreviousX or PreviousY property of a GameObject.
    /// </summary>
    public abstract class PreviousPositionWrapper : PropertyWrapper<int, GameObject> { }
    public class PreviousXWrapper : PreviousPositionWrapper
    {
        public override int Get(GameObject objectToWrap) => objectToWrap.PreviousX;
        public override void Set(GameObject objectToWrap, int value)
        {
            throw new NotSupportedException();
        }
    }
    public class PreviousYWrapper : PreviousPositionWrapper
    {
        public override int Get(GameObject objectToWrap) => objectToWrap.PreviousY;
        public override void Set(GameObject objectToWrap, int value)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Represents a PropertyWrapper for either the Width or Height property of an IRectangle.
    /// </summary>
    public abstract class SizeWrapper : PropertyWrapper<int, IRectangle> { }
    public class WidthWrapper : SizeWrapper
    {
        public override int Get(IRectangle objectToWrap) => objectToWrap.Width;
        public override void Set(IRectangle objectToWrap, int value) => objectToWrap.Width = value;
    }

    public class HeightWrapper : PropertyWrapper<int, IRectangle>
    {
        public override int Get(IRectangle objectToWrap) => objectToWrap.Height;
        public override void Set(IRectangle objectToWrap, int value) => objectToWrap.Height = value;
    }

    /// <summary>
    /// Represent a property wrapper for either the HSpeed or VSpeed property of a GameObject.
    /// </summary>
    public abstract class SpeedWrapper : PropertyWrapper<double, GameObject> { }
    public class VSpeedWrapper : SpeedWrapper
    {
        public override double Get(GameObject objectToWrap) => objectToWrap.VSpeed;
        public override void Set(GameObject objectToWrap, double value) => objectToWrap.VSpeed = value;
    }

    public class HSpeedWrapper : SpeedWrapper
    {
        public override double Get(GameObject objectToWrap) => objectToWrap.HSpeed;
        public override void Set(GameObject objectToWrap, double value) => objectToWrap.HSpeed = value;
    }
}
