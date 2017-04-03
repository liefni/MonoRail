using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    public static class Helpers
    {
        /// <summary>
        /// Checks if two rectangles intersect.
        /// </summary>
        /// <param name="r1">The first rectangle.</param>
        /// <param name="r2">The second rectangle.</param>
        /// <returns>Value indicating if rectangles intersect.</returns>
        public static bool Intersects(this IRectangle r1, IRectangle r2)
        {
            return (r2.X < r1.X + r1.Width) &&
                (r1.X < (r2.X + r2.Width)) &&
                (r2.Y < r1.Y + r1.Height) &&
                (r1.Y < r2.Y + r2.Height);
        }

        /// <summary>
        /// Populates all indexes of the array with a value.
        /// </summary>
        /// <typeparam name="T">The type of value in the array.</typeparam>
        /// <param name="array">The array to populate.</param>
        /// <param name="value">The value to fill the array with.</param>
        /// <returns>The populated array.</returns>
        public static T[] Populate<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }

        public static int GetRight(this IRectangle rectangle)
        {
            return rectangle.X + rectangle.Width;
        }

        public static int GetBottom(this IRectangle rectangle)
        {
            return rectangle.Y + rectangle.Height;
        }
    }
}
